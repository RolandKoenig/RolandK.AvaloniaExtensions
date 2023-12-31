using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Controls;

namespace RolandK.AvaloniaExtensions.Tests.Views;

[Collection(nameof(ApplicationTestCollection))]
public class DialogBoxControlTests
{
    [Fact]
    public Task Set_content_to_TestDialogControl()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
        });
    }

    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestDialogControl : UserControl
    {
        
    }
}