using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

// ReSharper disable RedundantCast

namespace RolandK.AvaloniaExtensions.ResponsiveControls;

public class ResponsiveGrid : BreakpointAwarePanel
{
    public static readonly AttachedProperty<int> ColumnsProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "Columns",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsSmProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsSm",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsMdProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsMd",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsLgProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsLg",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsXlProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsXl",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);
    public static readonly AttachedProperty<int> ColumnsXxlProperty =
        AvaloniaProperty.RegisterAttached<ResponsiveGrid, Control, int>(
            "ColumnsXxl",
            defaultValue: 0,
            validate: v => v is >= 0 and <= 12);

    public static readonly StyledProperty<HorizontalAlignment> RowAlignmentProperty =
        AvaloniaProperty.Register<ResponsiveGrid, HorizontalAlignment>(
            nameof(RowAlignment), 
            defaultValue: HorizontalAlignment.Left);
    
    public static readonly StyledProperty<double> RowSpacingProperty =
        AvaloniaProperty.Register<ResponsiveGrid, double>(
            nameof(RowSpacing), 
            defaultValue: 0d);
    public static readonly StyledProperty<double> ColumnSpacingProperty =
        AvaloniaProperty.Register<ResponsiveGrid, double>(
            nameof(ColumnSpacing), 
            defaultValue: 0d);
    
    private IReadOnlyList<ResponsiveGridRow> _currentRows = [];
    
    public HorizontalAlignment RowAlignment
    {
        get => GetValue(RowAlignmentProperty);
        set => SetValue(RowAlignmentProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the spacing between rows.
    /// </summary>
    public double RowSpacing
    {
        get => GetValue(RowSpacingProperty);
        set => SetValue(RowSpacingProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the spacing between columns.
    /// </summary>
    public double ColumnSpacing
    {
        get => GetValue(ColumnSpacingProperty);
        set => SetValue(ColumnSpacingProperty, value);
    }
    
    static ResponsiveGrid()
    {
        AffectsMeasure<ResponsiveGrid>(
            RowAlignmentProperty,
            RowSpacingProperty, ColumnSpacingProperty);
        AffectsParentMeasure<ResponsiveGrid>(
            ColumnsProperty, ColumnsSmProperty, ColumnsMdProperty,
            ColumnsLgProperty, ColumnsXlProperty, ColumnsXxlProperty);
    }
    
    public static void SetColumns(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsProperty, value);
    }
    
    public static int GetColumns(AvaloniaObject element)
    {
        return element.GetValue(ColumnsProperty);
    }
    
    public static void SetColumnsSm(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsSmProperty, value);
    }
    
    public static int GetColumnsSm(AvaloniaObject element)
    {
        return element.GetValue(ColumnsSmProperty);
    }
    
    public static void SetColumnsMd(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsMdProperty, value);
    }
    
    public static int GetColumnsMd(AvaloniaObject element)
    {
        return element.GetValue(ColumnsMdProperty);
    }
    
    public static void SetColumnsLg(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsLgProperty, value);
    }
    
    public static int GetColumnsLg(AvaloniaObject element)
    {
        return element.GetValue(ColumnsLgProperty);
    }
    
    public static void SetColumnsXl(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsXlProperty, value);
    }
    
    public static int GetColumnsXl(AvaloniaObject element)
    {
        return element.GetValue(ColumnsXlProperty);
    }
    
    public static void SetColumnsXxl(AvaloniaObject element, int value)
    {
        element.SetValue(ColumnsXxlProperty, value);
    }
    
    public static int GetColumnsXxl(AvaloniaObject element)
    {
        return element.GetValue(ColumnsXxlProperty);
    }
    
    protected override Size MeasureOverride(Size availableSize)
    {
        var singleColumnWidth = double.IsFinite(availableSize.Width)
            ? ((availableSize.Width - this.ColumnSpacing * 11.0) / 12.0)
            : double.PositiveInfinity;
        
        var fullBottomLine = 0d;
        var fullDesiredWith = 0d;
        _currentRows = this.CalculateRows();
        for(var loopRow = 0; loopRow < _currentRows.Count; loopRow++)
        {
            var actRow = _currentRows[loopRow];

            if (loopRow > 0)
            {
                fullBottomLine += this.RowSpacing;
            }
            
            var actRowBottomLine = 0d;
            var actRowDesiredWith = 0d;
            foreach (var actChild in actRow.Children)
            {
                var actChildControl = actChild.ChildControl!;
                actChildControl.Measure(new Size(
                    singleColumnWidth * actChild.ColumnCount + (this.ColumnSpacing * actChild.ColumnCount - 1), 
                    availableSize.Height));
                
                var actChildDesiredSize = actChildControl.DesiredSize;
                if (actChildDesiredSize.Height > actRowBottomLine)
                {
                    actRowBottomLine = actChildDesiredSize.Height;
                }
                actRowDesiredWith += actChildDesiredSize.Width;
            }

            actRowDesiredWith += (actRow.Children.Count - 1) * this.ColumnSpacing;

            fullBottomLine += actRowBottomLine;
            if (actRowDesiredWith > fullDesiredWith)
            {
                fullDesiredWith = actRowDesiredWith;
            }
        }
        
        return new Size(
            double.IsFinite(availableSize.Width)
                ? availableSize.Width
                : fullDesiredWith, 
            fullBottomLine);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var fullBottomYPosition = 0d;
        for (var loop = 0; loop < _currentRows.Count; loop++)
        {
            var actChild = _currentRows[loop];
            var actRowHeight = actChild.Children.Max(x => x.ChildControl!.DesiredSize.Height);

            if (loop > 0)
            {
                fullBottomYPosition += this.RowSpacing;
            }
            
            ArrangeRowChildren(
                actChild.Children,
                fullBottomYPosition,
                actRowHeight,
                finalSize.Width);

            fullBottomYPosition += actRowHeight;
        }
        
        return new Size(
            finalSize.Width, fullBottomYPosition);
    }
    
    private void ArrangeRowChildren(
        IReadOnlyList<ResponsiveGridRowChild> rowChildren, 
        double startYPosition, double rowHeight, double availableWidth)
    {
        var singleColumnWidth = (availableWidth - this.ColumnSpacing * 11.0) / 12.0;
        
        var startXPosition = 0d;
        var extendedWidthPerColumn = 0d;
        if (RowAlignment != HorizontalAlignment.Left)
        {
            var occupiedColumnCount = rowChildren.Sum(x => x.ColumnCount);
            var occupiedWidth = occupiedColumnCount * singleColumnWidth + (occupiedColumnCount - 1) * ColumnSpacing;
            var remainingWidth = availableWidth - occupiedWidth;
            if (remainingWidth > 0d)
            {
                startXPosition = this.RowAlignment switch
                {
                    HorizontalAlignment.Center => remainingWidth / 2d,
                    HorizontalAlignment.Right => remainingWidth,
                    _ => 0d
                };
                extendedWidthPerColumn = this.RowAlignment switch
                {
                    HorizontalAlignment.Stretch => remainingWidth / (double)occupiedColumnCount,
                    _ => 0d
                };
            }
        }
        
        var currentColumnIndex = 0;
        foreach(var actRowChild in rowChildren)
        {
            var spacingExtendedWidth = (actRowChild.ColumnCount - 1) * this.ColumnSpacing;
            
            var controlXPosition =
                startXPosition + 
                currentColumnIndex * (singleColumnWidth + extendedWidthPerColumn) + (currentColumnIndex * this.ColumnSpacing);
            var controlWidth = 
                actRowChild.ColumnCount * (singleColumnWidth + extendedWidthPerColumn) +
                spacingExtendedWidth;
            
            actRowChild.ChildControl!.Arrange(new Rect(
                new Point(controlXPosition, startYPosition),
                new Size(controlWidth, rowHeight)));
            currentColumnIndex += actRowChild.ColumnCount;
        }
    }

    private IReadOnlyList<ResponsiveGridRow> CalculateRows()
    {
        var result = new List<ResponsiveGridRow>(4);
        
        var actRow = new List<ResponsiveGridRowChild>(12);
        var actColumnCount = 0;
        var breakpoint = this.CurrentBreakpoint;
        foreach (var actChild in this.Children)
        {
            var actChildColumnCount = GetColumnCount(actChild, breakpoint);
            var columnCountForCalculation = actChildColumnCount > 0
                ? actChildColumnCount
                : 1;
            if (actColumnCount + columnCountForCalculation > 12)
            {
                // Transit to new row
                this.FillupFlexibleColumns(actRow);
                result.Add(new ResponsiveGridRow(actRow.ToArray()));
                actRow.Clear();
                actColumnCount = 0;
            }

            actColumnCount += columnCountForCalculation;
            actRow.Add(new ResponsiveGridRowChild(
                actChild, actChildColumnCount));
        }

        // Finish last row
        if (actRow.Count > 0)
        {
            this.FillupFlexibleColumns(actRow);
            result.Add(new ResponsiveGridRow(actRow.ToArray()));
        }

        return result;
    }

    private void FillupFlexibleColumns(IList<ResponsiveGridRowChild> children)
    {
        var totalOccupiedColumnCount = 0;
        var flexibleChildCount = 0;
        foreach (var actChild in children)
        {
            totalOccupiedColumnCount += actChild.ColumnCount;
            if(actChild.ColumnCount == 0){ flexibleChildCount++; }
        }
        
        // Cancel, if there is no flexible child
        if (flexibleChildCount == 0) { return; }

        // Cancel, if we've already occupied all columns
        if (totalOccupiedColumnCount == 12) { return; }
        
        var remainingColumns = 12 - totalOccupiedColumnCount;
        var extendableColumnCountPerChild = (double)remainingColumns / (double)flexibleChildCount;
        var remainingCountBecauseOfRounding = 0d;
        for (var loop=0; loop<children.Count; loop++)
        {
            var actChild = children[loop];
            if(actChild.ColumnCount > 0){ continue; }

            var currentlyCalculated = Math.Round(extendableColumnCountPerChild + remainingCountBecauseOfRounding);
            remainingCountBecauseOfRounding += extendableColumnCountPerChild - currentlyCalculated;
            
            children[loop] = new ResponsiveGridRowChild(
                actChild.ChildControl!,
                (int)currentlyCalculated);
        }
    }
    
    private int GetColumnCount(Control child, Breakpoint breakpoint)
    {
        var allBreakpoints = new[]
        {
            Breakpoint.Xs,
            Breakpoint.Sm,
            Breakpoint.Md,
            Breakpoint.Lg,
            Breakpoint.Xl,
            Breakpoint.Xxl
        };

        var columnCount = 0;
        for (var loop = 0; loop < allBreakpoints.Length; loop++)
        {
            var actColumnCount = GetColumnCountForBreakpoint(
                child, allBreakpoints[loop]);
            if (actColumnCount > 0)
            {
                columnCount = actColumnCount;
            }

            if (allBreakpoints[loop] == breakpoint)
            {
                break;
            }
        }
        
        if (columnCount > 12) { return 12; }
        return columnCount;
    }
    
    private int GetColumnCountForBreakpoint(Control child, Breakpoint breakpoint)
    {
        var result = breakpoint switch
        {
            Breakpoint.Xxl => child.GetValue(ColumnsXxlProperty),
            Breakpoint.Xl => child.GetValue(ColumnsXlProperty),
            Breakpoint.Lg => child.GetValue(ColumnsLgProperty),
            Breakpoint.Md => child.GetValue(ColumnsMdProperty),
            Breakpoint.Sm => child.GetValue(ColumnsSmProperty),
            _ => child.GetValue(ColumnsProperty),
        };
        
        if (result < 0) { return 0; }
        if (result > 12) { return 12; }
        return result;
    }
}