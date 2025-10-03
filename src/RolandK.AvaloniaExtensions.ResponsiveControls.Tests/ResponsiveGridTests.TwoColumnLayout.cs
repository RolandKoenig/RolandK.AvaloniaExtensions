using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Layout;

namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public partial class ResponsiveGridTests
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
    [InlineData(10, 2, 1000d, 200d)]
    [InlineData(8, 4, 800d, 400d)]
    [InlineData(6, 6, 600d, 600d)]
    public void TwoColumnLayout_WithMultipleRows(
        int leftColumns, int rightColumns, 
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        
        var leftChildRow1 = new Border();
        leftChildRow1.SetValue(ResponsiveGrid.ColumnsProperty, leftColumns);
        leftChildRow1.MinHeight = 100;
        responsiveGrid.Children.Add(leftChildRow1);
        
        var rightChildRow1 = new Border();
        rightChildRow1.SetValue(ResponsiveGrid.ColumnsProperty, rightColumns);
        rightChildRow1.MinHeight = 100;
        responsiveGrid.Children.Add(rightChildRow1);
        
        var leftChildRow2 = new Border();
        leftChildRow2.SetValue(ResponsiveGrid.ColumnsProperty, leftColumns);
        leftChildRow2.MinHeight = 100;
        responsiveGrid.Children.Add(leftChildRow2);
        
        var rightChildRow2 = new Border();
        rightChildRow2.SetValue(ResponsiveGrid.ColumnsProperty, rightColumns);
        rightChildRow2.MinHeight = 100;
        responsiveGrid.Children.Add(rightChildRow2);
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthLeft, leftChildRow1.Bounds.Width);
        Assert.Equal(expectedWidthLeft, leftChildRow2.Bounds.Width);
        Assert.Equal(0, leftChildRow1.Bounds.Top);
        Assert.Equal(100d, leftChildRow2.Bounds.Top);
        Assert.Equal(100d, leftChildRow1.Bounds.Height);
        Assert.Equal(100d, leftChildRow2.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChildRow1.Bounds.Width);
        Assert.Equal(expectedWidthRight, rightChildRow2.Bounds.Width);
        Assert.Equal(0, rightChildRow1.Bounds.Top);
        Assert.Equal(100d, rightChildRow2.Bounds.Top);
        Assert.Equal(100d, leftChildRow1.Bounds.Height);
        Assert.Equal(100d, leftChildRow2.Bounds.Height);
    }
    
    [AvaloniaTheory]
    [InlineData(10, 2, 1000d, 200d)]
    [InlineData(8, 4, 800d, 400d)]
    [InlineData(6, 6, 600d, 600d)]
    public void TwoColumnLayout_WithMultipleRows_WithRowSpacing(
        int leftColumns, int rightColumns, 
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        responsiveGrid.RowSpacing = 10d;
        
        var leftChildRow1 = new Border();
        leftChildRow1.SetValue(ResponsiveGrid.ColumnsProperty, leftColumns);
        leftChildRow1.MinHeight = 100;
        responsiveGrid.Children.Add(leftChildRow1);
        
        var rightChildRow1 = new Border();
        rightChildRow1.SetValue(ResponsiveGrid.ColumnsProperty, rightColumns);
        rightChildRow1.MinHeight = 100;
        responsiveGrid.Children.Add(rightChildRow1);
        
        var leftChildRow2 = new Border();
        leftChildRow2.SetValue(ResponsiveGrid.ColumnsProperty, leftColumns);
        leftChildRow2.MinHeight = 100;
        responsiveGrid.Children.Add(leftChildRow2);
        
        var rightChildRow2 = new Border();
        rightChildRow2.SetValue(ResponsiveGrid.ColumnsProperty, rightColumns);
        rightChildRow2.MinHeight = 100;
        responsiveGrid.Children.Add(rightChildRow2);
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = responsiveGrid;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthLeft, leftChildRow1.Bounds.Width);
        Assert.Equal(expectedWidthLeft, leftChildRow2.Bounds.Width);
        Assert.Equal(0, leftChildRow1.Bounds.Top);
        Assert.Equal(110d, leftChildRow2.Bounds.Top);
        Assert.Equal(100d, leftChildRow1.Bounds.Height);
        Assert.Equal(100d, leftChildRow2.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChildRow1.Bounds.Width);
        Assert.Equal(expectedWidthRight, rightChildRow2.Bounds.Width);
        Assert.Equal(0, rightChildRow1.Bounds.Top);
        Assert.Equal(110d, rightChildRow2.Bounds.Top);
        Assert.Equal(100d, leftChildRow1.Bounds.Height);
        Assert.Equal(100d, leftChildRow2.Bounds.Height);
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

    [AvaloniaTheory]
    [InlineData(HorizontalAlignment.Left, 0, 200, 200, 200)]
    [InlineData(HorizontalAlignment.Center, 400, 600, 200, 200)]
    [InlineData(HorizontalAlignment.Right, 800, 1000, 200, 200)]
    [InlineData(HorizontalAlignment.Stretch, 0, 600, 600, 600)]
    public void TwoColumnLayout_WithRemainingSpace(
        HorizontalAlignment rowAlignment,
        double expectedXPositionLeft, double expectedXPositionRight,
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        responsiveGrid.RowAlignment = rowAlignment;
        
        var leftChild = new Border();
        leftChild.SetValue(ResponsiveGrid.ColumnsProperty, 2);
        leftChild.MinHeight = 100;
        responsiveGrid.Children.Add(leftChild);
        
        var rightChild = new Border();
        rightChild.SetValue(ResponsiveGrid.ColumnsProperty, 2);
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
        Assert.Equal(expectedXPositionLeft, leftChild.Bounds.Left);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(expectedXPositionRight, rightChild.Bounds.Left);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
    
    [AvaloniaTheory]
    [InlineData(HorizontalAlignment.Left, 0, 220, 210, 210)]
    [InlineData(HorizontalAlignment.Center, 440, 660, 210, 210)]
    [InlineData(HorizontalAlignment.Right, 880, 1100, 210, 210)]
    [InlineData(HorizontalAlignment.Stretch, 0, 660, 650, 650)]
    public void TwoColumnLayout_WithRemainingSpace_WithSpacing(
        HorizontalAlignment rowAlignment,
        double expectedXPositionLeft, double expectedXPositionRight,
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        responsiveGrid.RowAlignment = rowAlignment;
        responsiveGrid.ColumnSpacing = 10d;
        
        var leftChild = new Border();
        leftChild.SetValue(ResponsiveGrid.ColumnsProperty, 2);
        leftChild.MinHeight = 100;
        responsiveGrid.Children.Add(leftChild);
        
        var rightChild = new Border();
        rightChild.SetValue(ResponsiveGrid.ColumnsProperty, 2);
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
        Assert.Equal(expectedXPositionLeft, leftChild.Bounds.Left);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(expectedXPositionRight, rightChild.Bounds.Left);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
    
    [AvaloniaTheory]
    [InlineData(HorizontalAlignment.Left, 0, 220, 210, 210)]
    [InlineData(HorizontalAlignment.Center, 440, 660, 210, 210)]
    [InlineData(HorizontalAlignment.Right, 880, 1100, 210, 210)]
    [InlineData(HorizontalAlignment.Stretch, 0, 660, 650, 650)]
    public void TwoColumnLayout_WithRemainingSpace_WithSpacing_OnSecondRow(
        HorizontalAlignment rowAlignment,
        double expectedXPositionLeft, double expectedXPositionRight,
        double expectedWidthLeft, double expectedWidthRight)
    {
        // Arrange
        var responsiveGrid = new ResponsiveGrid();
        responsiveGrid.RowAlignment = rowAlignment;
        responsiveGrid.ColumnSpacing = 10d;

        var firstRow = new Border();
        firstRow.SetValue(ResponsiveGrid.ColumnsProperty, 12);
        firstRow.MinHeight = 100;
        responsiveGrid.Children.Add(firstRow);
        
        var leftChild = new Border();
        leftChild.SetValue(ResponsiveGrid.ColumnsProperty, 2);
        leftChild.MinHeight = 100;
        responsiveGrid.Children.Add(leftChild);
        
        var rightChild = new Border();
        rightChild.SetValue(ResponsiveGrid.ColumnsProperty, 2);
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
        Assert.Equal(expectedXPositionLeft, leftChild.Bounds.Left);
        Assert.Equal(100d, leftChild.Bounds.Top);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(expectedXPositionRight, rightChild.Bounds.Left);
        Assert.Equal(100d, leftChild.Bounds.Top);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
}