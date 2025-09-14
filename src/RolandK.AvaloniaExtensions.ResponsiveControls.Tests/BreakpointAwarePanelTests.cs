using Avalonia;
using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public class BreakpointAwarePanelTests
{
    [AvaloniaTheory]
    [InlineData(100d, Breakpoint.Xs)]
    [InlineData(575d, Breakpoint.Xs)]
    [InlineData(576d, Breakpoint.Sm)]
    [InlineData(767d, Breakpoint.Sm)]
    [InlineData(768d, Breakpoint.Md)]
    [InlineData(991d, Breakpoint.Md)]
    [InlineData(992d, Breakpoint.Lg)]
    [InlineData(1199d, Breakpoint.Lg)]
    [InlineData(1200d, Breakpoint.Xl)]
    [InlineData(1399d, Breakpoint.Xl)]
    [InlineData(1400d, Breakpoint.Xxl)]
    [InlineData(1600d, Breakpoint.Xxl)]
    public void CheckForCorrectBreakpoint_DefaultSettings(double width, Breakpoint expectedBreakpoint)
    {
        // Arrange
        var panel = new BreakpointAwarePanel();
        
        // Act
        panel.Measure(new Size(width, 100.0));
        
        // Assert
        Assert.Equal(expectedBreakpoint, panel.CurrentBreakpoint);
    }
    
    [AvaloniaTheory]
    [InlineData(576d, ":breakpoint-sm")]
    [InlineData(768d, ":breakpoint-sm")]
    [InlineData(768d, ":breakpoint-md")]
    [InlineData(992d, ":breakpoint-sm")]
    [InlineData(992d, ":breakpoint-md")]
    [InlineData(992d, ":breakpoint-lg")]
    [InlineData(1200d, ":breakpoint-sm")]
    [InlineData(1200d, ":breakpoint-md")]
    [InlineData(1200d, ":breakpoint-lg")]
    [InlineData(1200d, ":breakpoint-xl")]
    [InlineData(1400d, ":breakpoint-sm")]
    [InlineData(1400d, ":breakpoint-md")]
    [InlineData(1400d, ":breakpoint-lg")]
    [InlineData(1400d, ":breakpoint-xl")]
    [InlineData(1400d, ":breakpoint-xxl")]
    public void CheckForContainingPseudoClass(double width, string expectedPseudoClass)
    {
        // Arrange
        var panel = new BreakpointAwarePanel();
        
        // Act
        panel.Measure(new Size(width, 100.0));
        
        // Assert
        Assert.Contains(expectedPseudoClass, panel.Classes);
    }
}