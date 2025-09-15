using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.ResponsiveControls.Tests;

public partial class ResponsiveGridTests
{
    [AvaloniaTheory]
    [InlineData(8, 4, 800d, 400d)]
    [InlineData(6, 6, 600d, 600d)]
    public void ParentControls_Border(
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

        var parentControl = new Border();
        parentControl.Padding = new Thickness(10);
        parentControl.Child = responsiveGrid;
        
        var parentWindow = new Window();
        parentWindow.Width = 1220;
        parentWindow.Height = 600;
        parentWindow.Content = parentControl;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthLeft, leftChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
    
    [AvaloniaTheory]
    [InlineData(8, 4, 800d, 400d)]
    [InlineData(6, 6, 600d, 600d)]
    public void ParentControls_VerticalScrollView(
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

        var parentControl = new ScrollViewer();
        parentControl.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        parentControl.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        parentControl.Content = responsiveGrid;
        
        var parentWindow = new Window();
        parentWindow.Width = 1200;
        parentWindow.Height = 600;
        parentWindow.Content = parentControl;
        
        // Act
        parentWindow.Show();
        
        // Assert
        Assert.Equal(expectedWidthLeft, leftChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
        Assert.Equal(expectedWidthRight, rightChild.Bounds.Width);
        Assert.Equal(100d, leftChild.Bounds.Height);
    }
}