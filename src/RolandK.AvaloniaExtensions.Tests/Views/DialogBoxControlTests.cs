using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;

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
            var testRoot = new TestRoot(dialogBoxControl);

            // Assert
            var ancestor = testRoot.FindLogicalDescendantOfType<TestDialogControl>();
            Assert.NotNull(ancestor);
        });
    }
    
    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestDialogControl : UserControl
    {
        
    }
}