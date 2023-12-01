using Avalonia.Controls;
using RolandK.AvaloniaExtensions.Mvvm.Markup;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices;

namespace RolandK.AvaloniaExtensions.Tests.ViewServices;

[Collection(nameof(ApplicationTestCollection))]
public class DefaultViewServicesTests
{
    [Fact]
    public Task Find_MessageBoxService_when_DialogHostControl_in_another_tree()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var rootGrid = new Grid();
            var localUserControl = new MvvmUserControl();
            var dialogHostControl = new DialogHostControl();
            rootGrid.Children.Add(localUserControl);
            rootGrid.Children.Add(dialogHostControl);

            // Act
            var testRoot = new TestRootWindow(rootGrid);
            var viewService =
                DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxViewService));

            // Assert
            Assert.NotNull(viewService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(viewService);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Find_MessageBoxService_when_DialogHostControl_in_MainWindowFrame()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var localUserControl = new MvvmUserControl();
            var mainWindowFrame = new MainWindowFrame(localUserControl);

            // Act
            var testRoot = new TestRootWindow(mainWindowFrame);
            var viewService =
                DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxViewService));

            // Assert
            Assert.NotNull(viewService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(viewService);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Find_MessageBoxService_when_DialogHostControl_is_parent()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var localUserControl = new MvvmUserControl();
            var dialogHostControl = new DialogHostControl();
            dialogHostControl.Children.Add(localUserControl);

            // Act
            var testRoot = new TestRootWindow(dialogHostControl);
            var viewService =
                DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxViewService));

            // Assert
            Assert.NotNull(viewService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(viewService);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Find_MessageBoxService_when_DialogHostControl_on_Window()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var localUserControl = new MvvmUserControl();
            var dialogHostControl = new DialogHostControl();
            dialogHostControl.Children.Add(localUserControl);

            // Act
            var testRoot = new TestRootWindow(dialogHostControl);
            var viewService =
                DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxViewService));

            // Assert
            Assert.NotNull(viewService);
            Assert.IsAssignableFrom<IMessageBoxViewService>(viewService);

            GC.KeepAlive(testRoot);
        });
    }

    [Fact]
    public Task Find_OpenFileDialogService_when_Window_is_parent()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var localUserControl = new MvvmUserControl();

            // Act
            var testRoot = new TestRootWindow(localUserControl);
            var viewService =
                DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IOpenFileViewService));

            // Assert
            Assert.NotNull(viewService);
            Assert.IsAssignableFrom<IOpenFileViewService>(viewService);

            GC.KeepAlive(testRoot);
        });
    }
    
    [Fact]
    public Task Find_SaveFileDialogService_when_Window_is_parent()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var localUserControl = new MvvmUserControl();

            // Act
            var testRoot = new TestRootWindow(localUserControl);
            var viewService =
                DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(ISaveFileViewService));

            // Assert
            Assert.NotNull(viewService);
            Assert.IsAssignableFrom<ISaveFileViewService>(viewService);

            GC.KeepAlive(testRoot);
        });
    }
}