using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace RolandK.AvaloniaExtensions.Views;

public class DialogHostControl : Grid
{
    public static readonly DirectProperty<DialogHostControl, Control?> OccludedControlProperty =
        AvaloniaProperty.RegisterDirect<DialogHostControl, Control?>(
            nameof(OccludedControl),
            o => o.OccludedControl,
            (o, v) => o.OccludedControl = v);

    private Stack<ChildInfo> _children;
    private Control? _occludedControl;
    private readonly IBrush _backgroundDialog;

    public Control? OccludedControl
    {
        get { return _occludedControl; }
        set { this.SetAndRaise(OccludedControlProperty, ref _occludedControl, value); }
    }

    public DialogHostControl()
    {
        _children = new Stack<ChildInfo>();
        _backgroundDialog = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));

        this.IsHitTestVisible = false;

    }

    public void ShowDialog(Control controlToShow, string headerText)
    {
        var currentChild = controlToShow;
        var currentChildInitialSize = new Size(currentChild.Width, currentChild.Height);

        var borderControl = new DialogBoxControl();
        borderControl.BorderThickness = new Thickness(1.0);
        borderControl.ContentArea.Add(currentChild);
        borderControl.HorizontalAlignment = HorizontalAlignment.Center;
        borderControl.VerticalAlignment = VerticalAlignment.Center;
        borderControl.Padding = new Thickness(5.0);
        borderControl.Header = headerText;
        var currentChildBorder = borderControl;

        var currentBackground = new Grid();
        currentBackground.Background = _backgroundDialog;

        _children.Push(new ChildInfo(
            currentChild, currentChildBorder,
            currentChildInitialSize, currentBackground));

        this.Children.Add(currentBackground);
        this.Children.Add(borderControl);
        this.IsHitTestVisible = true;

        if (this.OccludedControl != null)
        {
            this.OccludedControl.IsEnabled = false;
        }

        this.UpdateBorderSize();
    }

    public void CloseDialog()
    {
        if (_children.Count == 0) { return; }

        var removedChild = _children.Pop();
        this.Children.Remove(removedChild.ChildBorder);
        this.Children.Remove(removedChild.ChildBackground);

        if (_children.Count == 0)
        {
            this.IsHitTestVisible = false;

            if (this.OccludedControl != null)
            {
                this.OccludedControl.IsEnabled = true;
            }
        }
    }

    private void UpdateBorderSize()
    {
        const double BORDER_PADDING = 50.0;

        foreach (var actChildInfo in _children)
        {
            var currentChild = actChildInfo.Child;
            var currentChildInitialSize = actChildInfo.InitialSize;

            // Update height
            if (this.Bounds.Height < currentChildInitialSize.Height + BORDER_PADDING)
            {
                currentChild.Height = this.Bounds.Height - BORDER_PADDING;
            }
            else
            {
                currentChild.Height = currentChildInitialSize.Height;
            }

            // Update width
            if (this.Bounds.Width < currentChildInitialSize.Width + BORDER_PADDING)
            {
                currentChild.Width = this.Bounds.Width - BORDER_PADDING;
            }
            else
            {
                currentChild.Width = currentChildInitialSize.Width;
            }
        }
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == BoundsProperty)
        {
            this.UpdateBorderSize();
        }
    }

    //*********************************************************************
    //*********************************************************************
    //*********************************************************************
    public class ChildInfo
    {
        public Control Child { get; }

        public Control ChildBorder { get; }

        public Grid ChildBackground { get; }

        public Size InitialSize { get; }

        internal ChildInfo(
            Control child, Control childBorder,
            Size initialSize, Grid childBackground)
        {
            this.Child = child;
            this.ChildBorder = childBorder;
            this.InitialSize = initialSize;
            this.ChildBackground = childBackground;
        }
    }
}