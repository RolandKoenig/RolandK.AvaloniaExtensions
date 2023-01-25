using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Views;

public class MvvmUserControlTests
{
    [Fact]
    public void Attach_MvvmUserControl_To_ViewModel()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();
        
        // Act
        testMvvmControl.DataContext = testViewModel;
        var testRoot = new TestRoot(testMvvmControl);

        // Assert
        Assert.Equal(testMvvmControl, testViewModel.AssociatedView);
        
        GC.KeepAlive(testRoot);
    }

    [Fact]
    public void Attach_MvvmUserControl_To_ViewModel_Get_ViewService_MessageBox()
    {
        // Arrange
        var testMvvmControl = new MvvmUserControl();
        var testViewModel = new TestViewModel();
        
        // Act
        testMvvmControl.DataContext = testViewModel;
        var mainWindowFrame = new MainWindowFrame(testMvvmControl);
        var testRoot = new TestRoot(mainWindowFrame);
        var messageBoxService = testViewModel.TryGetViewService<IMessageBoxService>();
        
        // Assert
        Assert.NotNull(messageBoxService);
        Assert.IsAssignableFrom<IMessageBoxService>(messageBoxService);
        
        GC.KeepAlive(testRoot);
    }

    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestViewModel : ObservableObject, IAttachableViewModel
    {
#pragma warning disable CS0067
        /// <inheritdoc />
        public event EventHandler<CloseWindowRequestEventArgs>? CloseWindowRequest;
        
        /// <inheritdoc />
        public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;
#pragma  warning restore
        
        /// <inheritdoc />
        public object? AssociatedView { get; set; }

        public TViewService? TryGetViewService<TViewService>()
            where TViewService : class
        {
            var request = new ViewServiceRequestEventArgs(typeof(TViewService));
            this.ViewServiceRequest?.Invoke(this, request);
            return request.ViewService as TViewService;
        }
    }
}