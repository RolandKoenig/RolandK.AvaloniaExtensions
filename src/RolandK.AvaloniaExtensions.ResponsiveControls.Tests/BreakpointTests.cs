namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public class BreakpointTests
{
    [Theory]
    [InlineData(Breakpoint.Sm, Breakpoint.Xs)]
    [InlineData(Breakpoint.Md, Breakpoint.Sm)]
    [InlineData(Breakpoint.Lg, Breakpoint.Md)]
    [InlineData(Breakpoint.Xl, Breakpoint.Lg)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Xl)]
    public void BreakpointComparision_GreaterAs(Breakpoint left, Breakpoint right)
    {
        Assert.True(left > right);
    }
    
    [Theory]
    [InlineData(Breakpoint.Sm, Breakpoint.Xs)]
    [InlineData(Breakpoint.Sm, Breakpoint.Sm)]
    [InlineData(Breakpoint.Md, Breakpoint.Xs)]
    [InlineData(Breakpoint.Md, Breakpoint.Sm)]
    [InlineData(Breakpoint.Md, Breakpoint.Md)]
    [InlineData(Breakpoint.Lg, Breakpoint.Xs)]
    [InlineData(Breakpoint.Lg, Breakpoint.Sm)]
    [InlineData(Breakpoint.Lg, Breakpoint.Md)]
    [InlineData(Breakpoint.Lg, Breakpoint.Lg)]
    [InlineData(Breakpoint.Xl, Breakpoint.Xs)]
    [InlineData(Breakpoint.Xl, Breakpoint.Sm)]
    [InlineData(Breakpoint.Xl, Breakpoint.Md)]
    [InlineData(Breakpoint.Xl, Breakpoint.Lg)]
    [InlineData(Breakpoint.Xl, Breakpoint.Xl)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Xs)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Sm)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Md)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Lg)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Xl)]
    [InlineData(Breakpoint.Xxl, Breakpoint.Xxl)]
    public void BreakpointComparision_GreaterOrEqualAs(Breakpoint left, Breakpoint right)
    {
        Assert.True(left >= right);
    }
    
    [Theory]
    [InlineData(Breakpoint.Xs, Breakpoint.Sm)]
    [InlineData(Breakpoint.Sm, Breakpoint.Md)]
    [InlineData(Breakpoint.Md, Breakpoint.Lg)]
    [InlineData(Breakpoint.Lg, Breakpoint.Xl)]
    [InlineData(Breakpoint.Xl, Breakpoint.Xxl)]
    public void BreakpointComparision_SmallerAs(Breakpoint left, Breakpoint right)
    {
        Assert.True(left < right);
    }
}