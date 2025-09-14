using Avalonia.Controls;
using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public class ResponsiveGridTests
{
    [AvaloniaTheory]
    [InlineData(0, 2, 1000d, 200d)]
    [InlineData(10, 2, 1000d, 200d)]
    [InlineData(10, 0, 1000d, 200d)]
    [InlineData(0, 4, 800d, 400d)]
    [InlineData(8, 4, 800d, 400d)]
    [InlineData(8, 0, 800d, 400d)]
    [InlineData(6, 0, 600d, 600d)]
    [InlineData(6, 6, 600d, 600d)]
    [InlineData(0, 6, 600d, 600d)]
    [InlineData(0, 0, 600d, 600d)]
    public void TwoColumnLayout(
        int leftColumns, int rightColumns, 
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        
        var leftChild = new Border();
        leftChild.SetValue(ResponsiveGrid.ColumnsProperty, leftColumns);
        leftChild.MinHeight = 100;
        responsiveGrid.Children.Add(leftChild);
        
        var rightChild = new Border();
        rightChild.SetValue(ResponsiveGrid.ColumnsProperty, rightColumns);
        rightChild.MinHeight = 100;
        responsiveGrid.Children.Add(rightChild);
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthLeft, leftChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
    
    [AvaloniaTheory]
    [InlineData(0, 2, 1090d, 210d)]
    [InlineData(10, 2, 1090d, 210d)]
    [InlineData(10, 0, 1090d, 210d)]
    [InlineData(0, 4, 870d, 430d)]
    [InlineData(8, 4, 870d, 430d)]
    [InlineData(8, 0, 870d, 430d)]
    [InlineData(6, 0, 650d, 650d)]
    [InlineData(6, 6, 650d, 650d)]
    [InlineData(0, 6, 650d, 650d)]
    [InlineData(0, 0, 650d, 650d)]
    public void TwoColumnLayout_WithColumnSpacing(
        int leftColumns, int rightColumns, 
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        responsiveGrid.ColumnSpacing = 10d;
        
        var leftChild = new Border();
        leftChild.SetValue(ResponsiveGrid.ColumnsProperty, leftColumns);
        leftChild.MinHeight = 100;
        responsiveGrid.Children.Add(leftChild);
        
        var rightChild = new Border();
        rightChild.SetValue(ResponsiveGrid.ColumnsProperty, rightColumns);
        rightChild.MinHeight = 100;
        responsiveGrid.Children.Add(rightChild);
        
        var parentWindow = new Window();
        parentWindow.Width = 1310;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthLeft, leftChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
}