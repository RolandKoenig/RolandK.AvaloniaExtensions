using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Mvvm.Markup;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Mvvm;

[Collection(nameof(ApplicationTestCollection))]
public class MvvmUserControlTests
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
            var mainWindowFrame = new MainWindowFrame(testMvvmControl);
            var testRoot = new TestRootWindow(mainWindowFrame);
            var messageBoxService = testViewModel.TryGetViewService<IMessageBoxViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(messageBoxService);

            GC.KeepAlive(testRoot);
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