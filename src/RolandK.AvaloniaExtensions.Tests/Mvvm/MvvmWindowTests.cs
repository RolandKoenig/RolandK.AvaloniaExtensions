using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.Views;

[Collection(nameof(ApplicationTestCollection))]
public class MvvmWindowTests 
{
    [Fact]
    public async Task Attach_MvvmWindow_To_ViewModel()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            
            // Act
            var mvvmWindow = new MvvmWindow();
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.Show();
            
            // Assert
            Assert.True(mvvmWindow.IsVisible);
            Assert.Equal(testViewModel.AssociatedView, mvvmWindow);
        });
    }
    
    [Fact]
    public async Task Attach_MvvmUserControl_To_ViewModel_Get_ViewService_OpenFileDialog()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewModel = new TestViewModel();
            
            // Act
            var mvvmWindow = new MvvmWindow();
            mvvmWindow.DataContext = testViewModel;
            mvvmWindow.Show();
            
            var messageBoxService = testViewModel.TryGetViewService<IOpenFileViewService>();
            
            // Assert
            Assert.NotNull(messageBoxService);
            Assert.IsAssignableFrom<IOpenFileViewService>(messageBoxService);
        });
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