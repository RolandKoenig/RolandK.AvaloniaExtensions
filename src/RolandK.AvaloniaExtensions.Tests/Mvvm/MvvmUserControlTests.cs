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
public partial class MvvmUserControlTests
{
    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testMvvmControl = new MvvmUserControl();
            var testViewModel = new TestViewModel();

            // Act
            testMvvmControl.DataContext = testViewModel;
            testMvvmControl.ViewFor = typeof(TestViewModel);
            var testRoot = new TestRootWindow(testMvvmControl);
            
            // Assert
            Assert.Equal(testMvvmControl, testViewModel.AssociatedView);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel_then_detach()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testMvvmControl = new MvvmUserControl();
            var testViewModel = new TestViewModel();

            // Act
            testMvvmControl.DataContext = testViewModel;
            testMvvmControl.ViewFor = typeof(TestViewModel);
            var testRoot = new TestRootWindow(testMvvmControl);

            testRoot.Content = new Grid();

            // Assert
            Assert.Null(testViewModel.AssociatedView);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel_then_detach_with_Grid_in_control_hierarchy()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testMvvmControl = new MvvmUserControl();
            var testViewModel = new TestViewModel();

            // Act
            testMvvmControl.DataContext = testViewModel;
            testMvvmControl.ViewFor = typeof(TestViewModel);
            
            var mvvmControlContainer = new Grid();
            mvvmControlContainer.Children.Add(testMvvmControl);
            
            var testRoot = new TestRootWindow(mvvmControlContainer);

            testRoot.Content = new Grid();

            // Assert
            Assert.Null(testViewModel.AssociatedView);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel_then_close_parent_Window_using_ViewModel()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
        });
    }

    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel_get_ViewService_MessageBox()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testMvvmControl = new MvvmUserControl();
            var testViewModel = new TestViewModel();

            // Act
            testMvvmControl.DataContext = testViewModel;
            testMvvmControl.ViewFor = typeof(TestViewModel);
            var mainWindowFrame = new MainWindowFrame(testMvvmControl);
            var testRoot = new TestRootWindow(mainWindowFrame);
            var messageBoxService = testViewModel.TryGetViewService<IMessageBoxViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(messageBoxService);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Attach_ViewModel_to_multiple_views_throws_InvalidOperationException()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
        });
    }

    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel_then_detach_and_check_events_fired()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
        });
    }
    
    [Fact]
    public Task Attach_MvvmUserControl_to_ViewModel_then_check_ViewModelPropertyChanged_event()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
            var testRoot = new TestRootWindow(testMvvmControl);
            testViewModel.DummyProperty = "Some other value..";
            
            // Assert
            Assert.Equal(1, propertyChangedEventCount);
            Assert.Equal(nameof(TestViewModel.DummyProperty), lastPropertyChangedEventPropertyName);
            
            GC.KeepAlive(testRoot);
        });
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
}