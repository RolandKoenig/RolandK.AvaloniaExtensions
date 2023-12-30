using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Mvvm.Markup;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Views;

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
            mvvmWindow.Show();
            
            var messageBoxService = testViewModel.TryGetViewService<IMessageBoxViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(messageBoxService);
            
            // Cleanup
            mvvmWindow.Close();
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