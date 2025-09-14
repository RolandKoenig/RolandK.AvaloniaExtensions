using Avalonia.Controls;

namespace RolandK.AvaloniaExtensions.ResponsiveControls;

struct ResponsiveGridRowChild(Control childControl, int columnCount)
{
    public readonly Control? ChildControl = childControl;
    public readonly int ColumnCount = columnCount;
}