using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests.Markup;

public class MarkupExtensionWithDependencyInjectionTests
{
    [AvaloniaFact]
    public void Value_from_MarkupExtensionWithDependencyInjectionFakeExtension()
    {
        // Arrange
        var control = new MarkupExtensionWithDependencyInjectionTestControl();
        var mainWindow = new TestRootWindow(control);
        mainWindow.Show();
        
        // Assert
        var txtDummyValue = control.FindControl<TextBlock>("TxtDummyValue");
        Assert.NotNull(txtDummyValue);
        Assert.Equal("TestValue", txtDummyValue.Text);
    }
}