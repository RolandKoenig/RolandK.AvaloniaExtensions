using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Styling;

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
    [InlineData(15d, Breakpoint.Xs)]
    [InlineData(49d, Breakpoint.Xs)]
    [InlineData(50d, Breakpoint.Sm)]
    [InlineData(99d, Breakpoint.Sm)]
    [InlineData(100d, Breakpoint.Md)]
    [InlineData(149d, Breakpoint.Md)]
    [InlineData(150d, Breakpoint.Lg)]
    [InlineData(199d, Breakpoint.Lg)]
    [InlineData(200d, Breakpoint.Xl)]
    [InlineData(249d, Breakpoint.Xl)]
    [InlineData(250d, Breakpoint.Xxl)]
    [InlineData(300d, Breakpoint.Xxl)]
    public void CheckForCorrectBreakpoint_CustomSettings(double width, Breakpoint expectedBreakpoint)
    {
        // Arrange
        var window = new Window();

        var breakpointAwarePanelStyle = new Style();
        breakpointAwarePanelStyle.Selector = Selectors.OfType(null, typeof(BreakpointAwarePanel));
        breakpointAwarePanelStyle.Setters.Add(new Setter(
            BreakpointAwarePanel.BreakpointSmProperty, 50d));
        breakpointAwarePanelStyle.Setters.Add(new Setter(
            BreakpointAwarePanel.BreakpointMdProperty, 100d));
        breakpointAwarePanelStyle.Setters.Add(new Setter(
            BreakpointAwarePanel.BreakpointLgProperty, 150d));
        breakpointAwarePanelStyle.Setters.Add(new Setter(
            BreakpointAwarePanel.BreakpointXlProperty, 200d));
        breakpointAwarePanelStyle.Setters.Add(new Setter(
            BreakpointAwarePanel.BreakpointXxlProperty, 250d));
        window.Styles.Add(breakpointAwarePanelStyle);
        
        var panel = new BreakpointAwarePanel();
        window.Content = panel;
        window.Width = width;
        window.Show();
        
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