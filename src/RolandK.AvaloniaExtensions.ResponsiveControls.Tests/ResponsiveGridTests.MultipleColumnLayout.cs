using Avalonia.Controls;
using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public partial class ResponsiveGridTests
{
    [AvaloniaTheory]
    [InlineData(0, 0, 0, 400d, 400d, 400d)]
    [InlineData(5, 0, 0, 500d, 400d, 300d)]
    [InlineData(4, 4, 4, 400d, 400d, 400d)]
    public void MultipleColumnLayout_ThreeColumns(
        int firstColumns, int secondColumns, int thirdColumns,
        double expectedWidthFirst, double expectedWidthSecond, double expectedWidthThird)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        
        var firstChild = new Border();
        firstChild.SetValue(ResponsiveGrid.ColumnsProperty, firstColumns);
        firstChild.MinHeight = 100;
        responsiveGrid.Children.Add(firstChild);
        
        var secondChild = new Border();
        secondChild.SetValue(ResponsiveGrid.ColumnsProperty, secondColumns);
        secondChild.MinHeight = 100;
        responsiveGrid.Children.Add(secondChild);
        
        var thirdChild = new Border();
        thirdChild.SetValue(ResponsiveGrid.ColumnsProperty, thirdColumns);
        thirdChild.MinHeight = 100;
        responsiveGrid.Children.Add(thirdChild);
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthFirst, firstChild.Bounds.Width);
        Assert.Equal(100d, firstChild.Bounds.Height);
        Assert.Equal(expectedWidthSecond, secondChild.Bounds.Width);
        Assert.Equal(100d, secondChild.Bounds.Height);
        Assert.Equal(expectedWidthThird, thirdChild.Bounds.Width);
        Assert.Equal(100d, thirdChild.Bounds.Height);
    }
    
    [AvaloniaTheory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    public void MultipleColumnLayout_WithAutoCalculation(int countColumns)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();

        var children = new List<Border>(5);
        for (var loop = 0; loop < countColumns; loop++)
        {
            var actChild = new Border();
            actChild.SetValue(ResponsiveGrid.ColumnsProperty, 0);
            actChild.MinHeight = 100;
            responsiveGrid.Children.Add(actChild);

            children.Add(actChild);
        }
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        const double TOLERANCE = 0.0001d;
        Assert.True(Math.Abs(children.Sum(x => x.Bounds.Width) - 1200d) < TOLERANCE);
    }
    
    [AvaloniaTheory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    public void MultipleColumnLayout_WithAutoCalculation_WithSpacing(int countColumns)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        responsiveGrid.ColumnSpacing = 10d;

        var children = new List<Border>(5);
        for (var loop = 0; loop < countColumns; loop++)
        {
            var actChild = new Border();
            actChild.SetValue(ResponsiveGrid.ColumnsProperty, 0);
            actChild.MinHeight = 100;
            responsiveGrid.Children.Add(actChild);

            children.Add(actChild);
        }
        
        var parentWindow = new Window();
        parentWindow.Width = 1310;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        const double TOLERANCE = 0.0001d;
        var spaceSize = (countColumns - 1) * 10d;
        Assert.True(Math.Abs(children.Sum(x => x.Bounds.Width) - (1310d - spaceSize)) < TOLERANCE);
    }
}