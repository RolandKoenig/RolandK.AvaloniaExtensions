using Avalonia.Controls;
using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public partial class ResponsiveGridTests
{
    [AvaloniaFact]
    public void IsVisible_Support()
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        
        var firstChild = new Border();
        firstChild.SetValue(ResponsiveGrid.ColumnsProperty, 4);
        firstChild.MinHeight = 100;
        responsiveGrid.Children.Add(firstChild);
        
        var secondChild = new Border();
        secondChild.SetValue(ResponsiveGrid.ColumnsProperty, 4);
        secondChild.MinHeight = 100;
        secondChild.IsVisible = false;
        responsiveGrid.Children.Add(secondChild);
        
        var thirdChild = new Border();
        thirdChild.SetValue(ResponsiveGrid.ColumnsProperty, 4);
        thirdChild.MinHeight = 100;
        responsiveGrid.Children.Add(thirdChild);
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(0, firstChild.Bounds.X);
        Assert.Equal(400d, firstChild.Bounds.Width);
        Assert.Equal(100d, firstChild.Bounds.Height);
        Assert.Equal(0, secondChild.Bounds.X);
        Assert.Equal(0, secondChild.Bounds.Width);
        Assert.Equal(0, secondChild.Bounds.Height);
        Assert.Equal(400d, thirdChild.Bounds.X);
        Assert.Equal(400d, thirdChild.Bounds.Width);
        Assert.Equal(100d, thirdChild.Bounds.Height);
    }
}