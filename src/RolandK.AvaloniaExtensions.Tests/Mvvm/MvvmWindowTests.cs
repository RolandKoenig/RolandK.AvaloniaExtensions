using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Mvvm.Controls;
using RolandK.AvaloniaExtensions.Controls;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Mvvm;

public partial class MvvmWindowTests 
{
    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel()
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
        Assert.Equal(mvvmWindow, testViewModel.AssociatedView);
            
        // Cleanup
        mvvmWindow.Close();
    }
    
    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel_Discarded_in_DesignMode()
    {
        using(TestUtil.EnableDesignModeScope())
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
            Assert.Null(testViewModel.AssociatedView);
            
            // Cleanup
            mvvmWindow.Close();
        }
    }
    
    [AvaloniaFact]
    public void Attach_TestView_to_TestViewModel_by_conventions()
    {
        // Arrange
        var testViewModel = new TestViewModel();
        var mvvmWindow = new TestView();
            
        // Act
        mvvmWindow.DataContext = testViewModel;
        mvvmWindow.Show();
            
        // Assert
        Assert.True(mvvmWindow.IsVisible);
        Assert.Equal(mvvmWindow, testViewModel.AssociatedView);
            
        // Cleanup
        mvvmWindow.Close();
    }
    
    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel_then_close_using_ViewModel()
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
    }
    
    [AvaloniaFact]
    public async Task Attach_MvvmWindow_to_ViewModel_then_close_using_ViewModel_with_dialog_result()
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
    }
    
    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel_get_ViewService_OpenFileDialog()
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
    }

    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel_get_ViewService_SaveFileDialog()
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
    }
    
    [AvaloniaFact]
    public void Attach_MvvmWindow_with_MainWindowFrame_to_ViewModel_get_ViewService_MessageBox()
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
    }
    
    [AvaloniaFact]
    public void Attach_MvvmWindow_with_DialogHostControl_to_ViewModel_get_ViewService_MessageBox()
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
    }
    
    [AvaloniaFact]
    public void Attach_ViewModel_to_multiple_views_throws_InvalidOperationException()
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
    }
    
    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel_then_detach_and_check_events_fired()
    {
        // Arrange
        var testMvvmWindow = new MvvmWindow();
        var testViewModel = new TestViewModel();

        var viewModelAttachedEventCount = 0;
        var viewModelDetachedEventCount = 0;
        testMvvmWindow.ViewModelAttached += (_, _) => viewModelAttachedEventCount++;
        testMvvmWindow.ViewModelDetached += (_, _) => viewModelDetachedEventCount++;

        // Act
        testMvvmWindow.DataContext = testViewModel;
        testMvvmWindow.ViewFor = typeof(TestViewModel);
        testMvvmWindow.Show();
        testViewModel.TriggerCloseWindowRequest();
            
        // Assert
        Assert.Equal(1, viewModelAttachedEventCount);
        Assert.Equal(1, viewModelDetachedEventCount);
            
        // Cleanup
        testMvvmWindow.Close();
    }

    [AvaloniaFact]
    public void Attach_MvvmWindow_to_ViewModel_then_check_ViewModelPropertyChanged_event()
    {
        // Arrange
        var testMvvmWindow = new MvvmWindow();
        var testViewModel = new TestViewModel();

        var propertyChangedEventCount = 0;
        var lastPropertyChangedEventPropertyName = string.Empty;
        testMvvmWindow.ViewModelPropertyChanged += (_, args) =>
        {
            propertyChangedEventCount++;
            lastPropertyChangedEventPropertyName = args.PropertyName;
        };

        // Act
        testMvvmWindow.DataContext = testViewModel;
        testMvvmWindow.ViewFor = typeof(TestViewModel);
        testMvvmWindow.Show();
        testViewModel.DummyProperty = "Some other value..";

        // Assert
        Assert.Equal(1, propertyChangedEventCount);
        Assert.Equal(nameof(TestViewModel.DummyProperty), lastPropertyChangedEventPropertyName);

        testMvvmWindow.Close();
    }

    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private partial class TestViewModel : ObservableObject, IAttachableViewModel
    {
        /// <inheritdoc />
        public event EventHandler<CloseWindowRequestEventArgs>? CloseWindowRequest;
        
        /// <inheritdoc />
        public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

        /// <inheritdoc />
        public object? AssociatedView { get; set; }

        [ObservableProperty]
        private string _dummyProperty = string.Empty;
        
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

    private class TestView : MvvmWindow
    {
        
    }
}