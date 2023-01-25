using Avalonia.Controls;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices;

namespace RolandK.AvaloniaExtensions.Tests.ViewServices;

public class DefaultViewServicesTests
{
    [Fact]
    public void Find_MessageBoxService_when_DialogHostControl_in_another_tree()
    {
        // Arrange
        var rootGrid = new Grid();
        var localUserControl = new MvvmUserControl();
        var dialogHostControl = new DialogHostControl();
        rootGrid.Children.Add(localUserControl);
        rootGrid.Children.Add(dialogHostControl);

        // Act
        var testRoot = new TestRoot(rootGrid);
        var viewService = 
            DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxService));
        
        // Assert
        Assert.NotNull(viewService);
        Assert.IsAssignableFrom<IMessageBoxService>(viewService);

        GC.KeepAlive(testRoot);
    }
    
    [Fact]
    public void Find_MessageBoxService_when_DialogHostControl_in_MainWindowFrame()
    {
        // Arrange
        var localUserControl = new MvvmUserControl();
        var mainWindowFrame = new MainWindowFrame(localUserControl);

        // Act
        var testRoot = new TestRoot(mainWindowFrame);
        var viewService = 
            DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxService));
        
        // Assert
        Assert.NotNull(viewService);
        Assert.IsAssignableFrom<IMessageBoxService>(viewService);

        GC.KeepAlive(testRoot);
    }
    
    [Fact]
    public void Find_MessageBoxService_when_DialogHostControl_is_parent()
    {
        // Arrange
        var localUserControl = new MvvmUserControl();
        var dialogHostControl = new DialogHostControl();
        dialogHostControl.Children.Add(localUserControl);

        // Act
        var testRoot = new TestRoot(dialogHostControl);
        var viewService = 
            DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxService));
        
        // Assert
        Assert.NotNull(viewService);
        Assert.IsAssignableFrom<IMessageBoxService>(viewService);

        GC.KeepAlive(testRoot);
    }
    
    [Fact]
    public void Find_MessageBoxService_when_DialogHostControl_on_Window()
    {
        // Arrange
        var localUserControl = new MvvmUserControl();
        var dialogHostControl = new DialogHostControl();
        dialogHostControl.Children.Add(localUserControl);

        // Act
        var testRoot = new TestRootWindow(dialogHostControl);
        var viewService = 
            DefaultViewServices.TryGetDefaultViewService(localUserControl, typeof(IMessageBoxService));
        
        // Assert
        Assert.NotNull(viewService);
        Assert.IsAssignableFrom<IMessageBoxService>(viewService);

        GC.KeepAlive(testRoot);
    }

    [Fact]
    public void Find_OpenFileDialogService_when_Window_is_parent()
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
    }
    
    [Fact]
    public void Find_SaveFileDialogService_when_Window_is_parent()
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
    }
}