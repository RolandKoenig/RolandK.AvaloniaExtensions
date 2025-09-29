using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Mvvm.Controls;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Controls;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Mvvm;

public partial class MvvmUserControlTests
{
    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
        var testRootWindow = TestRootWindow.CreateAndShow(testMvvmControl);
            
        // Assert
        Assert.Equal(testMvvmControl, testViewModel.AssociatedView);
        
        GC.KeepAlive(testRootWindow);
    }
    
    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_Discarded_in_DesignMode()
    {
        using(TestUtil.EnableDesignModeScope())
        {
            // Arrange
            var testMvvmControl = new MvvmUserControl();
            var testViewModel = new TestViewModel();

            // Act
            testMvvmControl.DataContext = testViewModel;
            testMvvmControl.ViewFor = typeof(TestViewModel);
            var testRootWindow = TestRootWindow.CreateAndShow(testMvvmControl);

            // Assert
            Assert.Null(testViewModel.AssociatedView);

            GC.KeepAlive(testRootWindow);
        }
    }
    
    [AvaloniaFact]
    public void Attach_TestView_to_TestViewModel_by_conventions()
    {
        // Arrange
        var testView = new TestView();
        var testViewModel = new TestViewModel();

        // Act
        testView.DataContext = testViewModel;
        var testRootWindow = TestRootWindow.CreateAndShow(testView);
            
        // Assert
        Assert.Equal(testView, testViewModel.AssociatedView);

        GC.KeepAlive(testRootWindow);
    }
    
    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_then_detach()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
        var testRootWindow = TestRootWindow.CreateAndShow(testMvvmControl);

        testRootWindow.Content = new Grid();

        // Assert
        Assert.Null(testViewModel.AssociatedView);

        GC.KeepAlive(testRootWindow);
    }
    
    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_then_detach_with_Grid_in_control_hierarchy()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
            
        var mvvmControlContainer = new Grid();
        mvvmControlContainer.Children.Add(testMvvmControl);
            
        var testRootWindow = TestRootWindow.CreateAndShow(mvvmControlContainer);
        testRootWindow.Content = new Grid();

        // Assert
        Assert.Null(testViewModel.AssociatedView);

        GC.KeepAlive(testRootWindow);
    }
    
    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_then_close_parent_Window_using_ViewModel()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();
            
        var parentWindow = new Window();
        parentWindow.Content = testMvvmControl;
        parentWindow.Show();

        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
        testViewModel.TriggerCloseWindowRequest();

        parentWindow.Content = null;

        // Assert
        Assert.Null(testViewModel.AssociatedView);
        Assert.False(parentWindow.IsVisible);
    }

    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_get_ViewService_MessageBox()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
        var mainWindowFrame = new MainWindowFrame(testMvvmControl);
        var testRootWindow = TestRootWindow.CreateAndShow(mainWindowFrame);
        var messageBoxService = testViewModel.TryGetViewService<IMessageBoxViewService>();
            
        // Assert
        Assert.NotNull(messageBoxService);
        Assert.IsAssignableFrom<IMessageBoxViewService>(messageBoxService);

        GC.KeepAlive(testRootWindow);
    }
    
    [AvaloniaFact]
    public void Attach_ViewModel_to_multiple_views_throws_InvalidOperationException()
    {
        // Arrange
        var testMvvmControl1 = new MvvmUserControl();
        var testMvvmControl2 = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        // Act
        Exception? catchedException = null;
        try
        {
            testMvvmControl1.DataContext = testViewModel;
            testMvvmControl1.ViewFor = typeof(TestViewModel);
            testMvvmControl2.DataContext = testViewModel;
            testMvvmControl2.ViewFor = typeof(TestViewModel);

            var testPanel = new Panel();
            testPanel.Children.Add(testMvvmControl1);
            testPanel.Children.Add(testMvvmControl2);

            _ = new TestRootWindow(testPanel);
        }
        catch (Exception ex)
        {
            catchedException = ex;
        }
            
        // Assert
        Assert.NotNull(catchedException);
        var invalidOperationException = Assert.IsType<InvalidOperationException>(catchedException);
        Assert.Contains("DataContext", invalidOperationException.Message);
        Assert.Contains("MvvmUserControl", invalidOperationException.Message);
        Assert.Contains("TestViewModel", invalidOperationException.Message);
    }

    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_then_detach_and_check_events_fired()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        var viewModelAttachedEventCount = 0;
        var viewModelDetachedEventCount = 0;
        testMvvmControl.ViewModelAttached += (_, _) => viewModelAttachedEventCount++;
        testMvvmControl.ViewModelDetached += (_, _) => viewModelDetachedEventCount++;
            
        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
        var testRoot = new TestRootWindow(testMvvmControl);
        testRoot.Content = new Grid();
            
        // Assert
        Assert.Equal(1, viewModelAttachedEventCount);
        Assert.Equal(1, viewModelDetachedEventCount);
            
        GC.KeepAlive(testRoot);
    }
    
    [AvaloniaFact]
    public void Attach_MvvmUserControl_to_ViewModel_then_check_ViewModelPropertyChanged_event()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();

        var propertyChangedEventCount = 0;
        var lastPropertyChangedEventPropertyName = string.Empty;
        testMvvmControl.ViewModelPropertyChanged += (_, args) =>
        {
            propertyChangedEventCount++;
            lastPropertyChangedEventPropertyName = args.PropertyName;
        };
            
        // Act
        testMvvmControl.DataContext = testViewModel;
        testMvvmControl.ViewFor = typeof(TestViewModel);
        var testRoot = TestRootWindow.CreateAndShow(testMvvmControl);
        testViewModel.DummyProperty = "Some other value..";
            
        // Assert
        Assert.Equal(1, propertyChangedEventCount);
        Assert.Equal(nameof(TestViewModel.DummyProperty), lastPropertyChangedEventPropertyName);
            
        GC.KeepAlive(testRoot);
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

    private class TestView : MvvmUserControl
    {
        
    }
}