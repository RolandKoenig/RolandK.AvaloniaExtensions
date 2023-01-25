using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Styling;
using Avalonia.VisualTree;
using NSubstitute;

namespace RolandK.AvaloniaExtensions.Tests.Util;

// Original code from:
// https://github.com/AvaloniaUI/Avalonia/blob/ec74057151e8f405bbc8a324325a9f957e84bf7b/tests/Avalonia.UnitTests/TestRoot.cs

public class TestRoot : Decorator, IFocusScope, ILayoutRoot, IInputRoot, IRenderRoot, IStyleHost, ILogicalRoot
{
    private readonly NameScope _nameScope = new NameScope();

    public Size ClientSize { get; set; } = new Size(1000, 1000);

    public Size MaxClientSize { get; set; } = Size.Infinity;

    public double LayoutScaling { get; set; } = 1;

    public ILayoutManager LayoutManager { get; set; }

    public double RenderScaling => 1;

    public IRenderer Renderer { get; set; }

    public IAccessKeyHandler AccessKeyHandler => null!;

    public IKeyboardNavigationHandler KeyboardNavigationHandler => null!;

    public IInputElement? PointerOverElement { get; set; }

    public bool ShowAccessKeys { get; set; }

    /// <inheritdoc />
    public IMouseDevice? MouseDevice { get; } = null;

    public IStyleHost? StylingParent { get; set; }

    IStyleHost? IStyleHost.StylingParent => this.StylingParent;

    public TestRoot()
    {
        this.Renderer = Substitute.For<IRenderer>();
        this.LayoutManager = new LayoutManager(this);
        this.IsVisible = true;

        KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
    }

    public TestRoot(Control child)
        : this()
    {
        this.Child = child;
    }
    
    public IRenderTarget CreateRenderTarget()
    {
        var dc = Substitute.For<IDrawingContextImpl>();
        dc.CreateLayer(Arg.Any<Size>()).Returns(_ =>
        {
            var layerDc = Substitute.For<IDrawingContextImpl>();
            var layer = Substitute.For<IDrawingContextLayerImpl>();
            layer.CreateDrawingContext(Arg.Any<IVisualBrushRenderer>()).Returns(layerDc);
            return layer;
        });
        
        var result = Substitute.For<IRenderTarget>();
        result.CreateDrawingContext(Arg.Any<IVisualBrushRenderer>()).Returns(dc);
        return result;
    }

    public void Invalidate(Rect rect)
    {
    }

    public Point PointToClient(PixelPoint p) => p.ToPoint(1);

    public PixelPoint PointToScreen(Point p) => PixelPoint.FromPoint(p, 1);

    public void RegisterChildrenNames()
    {
        var scope = NameScope.GetNameScope(this) ?? new NameScope();
        NameScope.SetNameScope(this, scope);

        void Visit(StyledElement element, bool force = false)
        {
            if (element.Name != null)
            {
                if (!ReferenceEquals(scope.Find(element.Name), element))
                {
                    scope.Register(element.Name, element);
                }
            }

            if (element is Visual visual && (force || NameScope.GetNameScope(element) == null))
                foreach (var child in visual.GetVisualChildren())
                    if (child is StyledElement styledChild)
                        Visit(styledChild);
        }

        Visit(this, true);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        return base.MeasureOverride(ClientSize);
    }
}