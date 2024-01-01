using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Mvvm.Controls;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Controls;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Mvvm;

[Collection(nameof(ApplicationTestCollection))]
public class MvvmWindowTests 
{
    [Fact]
    public async Task Attach_MvvmWindow_to_ViewModel()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            mvvmWindow.Show();
            
            // Assert
            Assert.True(mvvmWindow.IsVisible);
            Assert.Equal(testViewModel.AssociatedView, mvvmWindow);
            
            // Cleanup
            mvvmWindow.Close();
        });
    }
    
    [Fact]
    public async Task Attach_MvvmWindow_to_ViewModel_then_close_using_ViewModel()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            mvvmWindow.Show();
            
            testViewModel.TriggerCloseWindowRequest();
            
            // Assert
            Assert.False(mvvmWindow.IsVisible);
            Assert.Null(testViewModel.AssociatedView);
            
            // Cleanup
            mvvmWindow.Close();
        });
    }
    
    [Fact]
    public async Task Attach_MvvmWindow_to_ViewModel_then_close_using_ViewModel_with_dialog_result()
    {
        await UnitTestApplication.RunInApplicationContextAsync(async Task () =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            var topLevelWindow = new Window();
            topLevelWindow.Show();
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            var showDialogTask = mvvmWindow.ShowDialog<object>(topLevelWindow);

            var dialogResult = new object();
            testViewModel.TriggerCloseWindowRequest(dialogResult);

            var showDialogTaskResult = await showDialogTask;
            
            // Assert
            Assert.Equal(TaskStatus.RanToCompletion, showDialogTask.Status);
            Assert.Equal(dialogResult, showDialogTaskResult);
            Assert.False(mvvmWindow.IsVisible);
            Assert.Null(testViewModel.AssociatedView);
            
            // Cleanup
            topLevelWindow.Close();
        });
    }
    
    [Fact]
    public async Task Attach_MvvmWindow_to_ViewModel_get_ViewService_OpenFileDialog()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            mvvmWindow.Show();
            
            var messageBoxService = testViewModel.TryGetViewService<IOpenFileViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IOpenFileViewService>(messageBoxService);
            
            // Cleanup
            mvvmWindow.Close();
        });
    }

    [Fact]
    public async Task Attach_MvvmWindow_to_ViewModel_get_ViewService_SaveFileDialog()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            mvvmWindow.Show();
            
            var messageBoxService = testViewModel.TryGetViewService<ISaveFileViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<ISaveFileViewService>(messageBoxService);
            
            // Cleanup
            mvvmWindow.Close();
        });
    }
    
    [Fact]
    public async Task Attach_MvvmWindow_with_MainWindowFrame_to_ViewModel_get_ViewService_MessageBox()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            
            var mainWindowFrame = new MainWindowFrame();
            mvvmWindow.Content = mainWindowFrame;
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            mvvmWindow.Show();
            
            var messageBoxService = testViewModel.TryGetViewService<IMessageBoxViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(messageBoxService);
            
            // Cleanup
            mvvmWindow.Close();
        });
    }
    
    [Fact]
    public async Task Attach_MvvmWindow_with_DialogHostControl_to_ViewModel_get_ViewService_MessageBox()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow = new MvvmWindow();
            
            var dialogHostControl = new DialogHostControl();
            mvvmWindow.Content = dialogHostControl;
            
            // Act
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.ViewFor = typeof(TestViewModel);
            mvvmWindow.Show();
            
            var messageBoxService = testViewModel.TryGetViewService<IMessageBoxViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(messageBoxService);
            
            // Cleanup
            mvvmWindow.Close();
        });
    }
    
    [Fact]
    public Task Attach_ViewModel_to_multiple_views_throws_InvalidOperationException()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            var mvvmWindow1 = new MvvmWindow();
            var mvvmWindow2 = new MvvmWindow();
            
            // Act
            Exception? catchedException = null;
            try
            {
                mvvmWindow1.DataContext = testViewModel;
                mvvmWindow1.ViewFor = typeof(TestViewModel);
                mvvmWindow1.Show();

                mvvmWindow2.DataContext = testViewModel;
                mvvmWindow2.ViewFor = typeof(TestViewModel);
                mvvmWindow2.Show();
            }
            catch (Exception ex)
            {
                catchedException = ex;
            }
            
            // Assert
            Assert.NotNull(catchedException);
            var invalidOperationException = Assert.IsType<InvalidOperationException>(catchedException);
            Assert.Contains("DataContext", invalidOperationException.Message);
            Assert.Contains("MvvmWindow", invalidOperationException.Message);
            Assert.Contains("TestViewModel", invalidOperationException.Message);
        });
    }
    
    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestViewModel : ObservableObject, IAttachableViewModel
    {
        /// <inheritdoc />
        public event EventHandler<CloseWindowRequestEventArgs>? CloseWindowRequest;
        
        /// <inheritdoc />
        public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

        /// <inheritdoc />
        public object? AssociatedView { get; set; }

        public TViewService? TryGetViewService<TViewService>()
            where TViewService : class
        {
            var request = new ViewServiceRequestEventArgs(typeof(TViewService));
            this.ViewServiceRequest?.Invoke(this, request);
            return request.ViewService as TViewService;
        }

        public void TriggerCloseWindowRequest(object? dialogResult = null)
        {
            this.CloseWindowRequest?.Invoke(this, new CloseWindowRequestEventArgs(dialogResult));
        }
    }
}