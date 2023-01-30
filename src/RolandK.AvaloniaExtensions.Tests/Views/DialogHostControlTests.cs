using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;

namespace RolandK.AvaloniaExtensions.Tests.Views;

[Collection(nameof(ApplicationTestCollection))]
public class DialogHostControlTests
{
    [Fact]
    public Task Show_Dialog_using_DialogControlHost()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            
            // Assert
            var ancestor = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
            Assert.NotNull(ancestor);
        });
    }
    
    [Fact]
    public Task Show_Dialog_using_DialogControlHost_then_close()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            dialogHostControl.CloseDialog();
            
            // Assert
            var ancestor = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
            Assert.Null(ancestor);
        });
    }
    
    [Fact]
    public Task Show_Dialog_using_DialogControlHost_then_show_another_Dialog()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            dialogHostControl.ShowDialog(new AnotherTestDialogControl(), "Another dialog");
            
            // Assert
            var firstDialogControl = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
            var anotherDialogControl = testRoot.FindLogicalDescendantOfType<AnotherTestDialogControl>();
            Assert.NotNull(firstDialogControl);
            Assert.NotNull(anotherDialogControl);
        });
    }
    
    [Fact]
    public Task Show_Dialog_using_DialogControlHost_then_show_another_Dialog_then_close_one()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            dialogHostControl.ShowDialog(new AnotherTestDialogControl(), "Another dialog");
            dialogHostControl.CloseDialog();
            
            // Assert
            var firstDialogControl = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
            var anotherDialogControl = testRoot.FindLogicalDescendantOfType<AnotherTestDialogControl>();
            Assert.NotNull(firstDialogControl);
            Assert.Null(anotherDialogControl);
        });
    }
    
    [Fact]
    public Task Show_Dialog_using_DialogControlHost_then_show_another_Dialog_then_close_all()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            dialogHostControl.ShowDialog(new AnotherTestDialogControl(), "Another dialog");
            dialogHostControl.CloseDialog();
            dialogHostControl.CloseDialog();
            
            // Assert
            var firstDialogControl = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
            var anotherDialogControl = testRoot.FindLogicalDescendantOfType<AnotherTestDialogControl>();
            Assert.Null(firstDialogControl);
            Assert.Null(anotherDialogControl);
        });
    }
    
    [Fact]
    public Task Disable_OccludedControl_when_showing_dialog()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var occludedControl = new UserControl();
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);
            dialogHostControl.OccludedControl = occludedControl;

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            
            // Assert
            Assert.False(occludedControl.IsEnabled);
        });
    }
    
    [Fact]
    public Task Enable_OccludedControl_when_closing_dialog()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var occludedControl = new UserControl();
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);
            dialogHostControl.OccludedControl = occludedControl;

            dialogHostControl.ShowDialog(new TestDialogControl(), "My dummy header");
            dialogHostControl.CloseDialog();
            
            // Assert
            Assert.True(occludedControl.IsEnabled);
        });
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