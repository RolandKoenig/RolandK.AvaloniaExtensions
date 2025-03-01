using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Controls;

namespace RolandK.AvaloniaExtensions.Tests.Views;

public class DialogBoxControlTests
{
    [AvaloniaFact]
    public void Set_content_to_TestDialogControl()
    {
        // Arrange
        var dialogBoxControl = new DialogBoxControl();

        // Act
        dialogBoxControl.Header = "My dummy header";
        dialogBoxControl.ContentArea.Add(new TestDialogControl());
        var testRoot = new TestRootWindow(dialogBoxControl);

        // Assert
        var ancestor = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
        Assert.NotNull(ancestor);

        var textBoxes =
            TestUtil.FindLogicalDescendantsOfType<TextBlock>(testRoot);
        var headerTextBox = textBoxes.FirstOrDefault(x => x.Text == "My dummy header");
        Assert.NotNull(headerTextBox);
    }

    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestDialogControl : UserControl
    {
        
    }
}