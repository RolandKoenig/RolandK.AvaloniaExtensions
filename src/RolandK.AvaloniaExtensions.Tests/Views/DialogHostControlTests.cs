using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Controls;

namespace RolandK.AvaloniaExtensions.Tests.Views;

public class DialogHostControlTests
{
    [AvaloniaFact]
    public void Show_Dialog_using_DialogControlHost()
    {
        // Arrange
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            
        // Assert
        var ancestor = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
        Assert.NotNull(ancestor);
    }
    
    [AvaloniaFact]
    public void Show_Dialog_using_DialogControlHost_then_close()
    {
        // Arrange
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
        dialogHostControl.CloseDialog();
            
        // Assert
        var ancestor = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
        Assert.Null(ancestor);
    }
    
    [AvaloniaFact]
    public void Show_Dialog_using_DialogControlHost_then_show_another_Dialog()
    {
        // Arrange
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
        dialogHostControl.ShowDialog(new AnotherTestDialogControl(), "Another dialog");
            
        // Assert
        var firstDialogControl = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
        var anotherDialogControl = testRoot.FindLogicalDescendantOfType<AnotherTestDialogControl>();
        Assert.NotNull(firstDialogControl);
        Assert.NotNull(anotherDialogControl);
    }
    
    [AvaloniaFact]
    public void Show_Dialog_using_DialogControlHost_then_show_another_Dialog_then_close_one()
    {
        // Arrange
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
        dialogHostControl.ShowDialog(new AnotherTestDialogControl(), "Another dialog");
        dialogHostControl.CloseDialog();
            
        // Assert
        var firstDialogControl = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
        var anotherDialogControl = testRoot.FindLogicalDescendantOfType<AnotherTestDialogControl>();
        Assert.NotNull(firstDialogControl);
        Assert.Null(anotherDialogControl);
    }
    
    [AvaloniaFact]
    public void Show_Dialog_using_DialogControlHost_then_show_another_Dialog_then_close_all()
    {
        // Arrange
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
        dialogHostControl.ShowDialog(new AnotherTestDialogControl(), "Another dialog");
        dialogHostControl.CloseDialog();
        dialogHostControl.CloseDialog();
            
        // Assert
        var firstDialogControl = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
        var anotherDialogControl = testRoot.FindLogicalDescendantOfType<AnotherTestDialogControl>();
        Assert.Null(firstDialogControl);
        Assert.Null(anotherDialogControl);
    }
    
    [AvaloniaFact]
    public void Disable_OccludedControl_when_showing_dialog()
    {
        // Arrange
        var occludedControl = new UserControl();
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);
        dialogHostControl.OccludedControl = occludedControl;

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            
        // Assert
        Assert.False(occludedControl.IsEnabled);
            
        GC.KeepAlive(testRoot);
    }
    
    [AvaloniaFact]
    public void Enable_OccludedControl_when_closing_dialog()
    {
        // Arrange
        var occludedControl = new UserControl();
        var dialogHostControl = new DialogHostControl();
            
        // Act
        var testRoot = new TestRootWindow(dialogHostControl);
        dialogHostControl.OccludedControl = occludedControl;

        dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
        dialogHostControl.CloseDialog();

        // Assert
        Assert.True(occludedControl.IsEnabled);

        GC.KeepAlive(testRoot);
    }
    
    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestDialogControl : UserControl
    {
        
    }
    
    private class AnotherTestDialogControl : UserControl
    {
        
    }
}