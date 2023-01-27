using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;

namespace RolandK.AvaloniaExtensions.Tests.Views;

[Collection(nameof(ApplicationTestCollection))]
public class DialogHostControlTests
{
    [Fact]
    public Task Add_Control_to_DialogControlHost()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var dialogHostControl = new DialogHostControl();
            
            // Act
            var testRoot = new TestRoot(dialogHostControl);
            testRoot.Child = dialogHostControl;

            dialogHostControl.ShowDialog(new Calendar(), "My dummy header");
            
            // Assert
            var ancestor = testRoot.FindLogicalDescendantOfType<Calendar>();
            Assert.NotNull(ancestor);
        });
    }
}