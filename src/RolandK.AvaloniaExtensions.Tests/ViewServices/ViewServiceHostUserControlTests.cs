using RolandK.AvaloniaExtensions.Mvvm.Controls;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Tests.ViewServices;

[Collection(nameof(ApplicationTestCollection))]
public class ViewServiceHostUserControlTests
{
    [Fact]
    public Task DefaultViewService_from_ViewServiceHostUserControl()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewServiceUserControl = new ViewServiceHostUserControl();

            var hostWindow = new TestRootWindow();
            hostWindow.Content = testViewServiceUserControl;

            // Act
            var srvOpenFileDialog = testViewServiceUserControl.TryFindViewService<IOpenFileViewService>();

            // Assert
            Assert.NotNull(srvOpenFileDialog);

            GC.KeepAlive(hostWindow);
        });
    }

    [Fact]
    public Task Find_ViewService_From_ParentContainer()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var testViewServiceUserControl = new ViewServiceHostUserControl();

            var parentViewServiceUserControl = new ViewServiceHostUserControl();
            parentViewServiceUserControl.Content = testViewServiceUserControl;
            parentViewServiceUserControl.ViewServices.Add(new DummyViewService());

            var hostWindow = new TestRootWindow();
            hostWindow.Content = parentViewServiceUserControl;

            // Act
            var srvOpenFileDialog = testViewServiceUserControl.TryFindViewService<IDummyViewService>();

            // Assert
            Assert.NotNull(srvOpenFileDialog);

            GC.KeepAlive(hostWindow);
        });
    }

    private interface IDummyViewService : IViewService { }

    private class DummyViewService : ViewServiceBase, IDummyViewService { }
}
