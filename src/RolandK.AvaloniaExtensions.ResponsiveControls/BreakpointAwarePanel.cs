using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Data;

namespace RolandK.AvaloniaExtensions.ResponsiveControls;

[PseudoClasses(
    ":breakpoint-sm",
    ":breakpoint-md",
    ":breakpoint-lg",
    ":breakpoint-xl",
    ":breakpoint-xxl")]
public class BreakpointAwarePanel : Panel
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointSmProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointSm), 
            defaultValue: 576d);
    public double BreakpointSm
    {
        get => this.GetValue(BreakpointSmProperty);
        set => this.SetValue(BreakpointSmProperty, value);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointMdProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointMd), 
            defaultValue: 768d);
    public double BreakpointMd
    {
        get => this.GetValue(BreakpointMdProperty);
        set => this.SetValue(BreakpointMdProperty, value);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointLgProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointLg), 
            defaultValue: 992d);
    public double BreakpointLg
    {
        get => this.GetValue(BreakpointLgProperty);
        set => this.SetValue(BreakpointLgProperty, value);
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointXlProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointXl), 
            defaultValue: 1200d);
    public double BreakpointXl
    {
        get => this.GetValue(BreakpointXlProperty);
        set => this.SetValue(BreakpointXlProperty, value);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly StyledProperty<double> BreakpointXxlProperty =
        AvaloniaProperty.Register<BreakpointAwarePanel, double>(
            nameof(BreakpointXxl), 
            defaultValue: 1400d);
    public double BreakpointXxl
    {
        get => this.GetValue(BreakpointXxlProperty);
        set => this.SetValue(BreakpointXxlProperty, value);
    }
    
    /// <summary>
    /// The Breakpoints which was calculated through the last Measure pass.
    /// </summary>
    public static readonly DirectProperty<BreakpointAwarePanel, Breakpoint> CurrentBreakpointProperty =
        AvaloniaProperty.RegisterDirect<BreakpointAwarePanel, Breakpoint>(
            nameof(CurrentBreakpoint),
            o => o.CurrentBreakpoint,
            defaultBindingMode: BindingMode.OneWayToSource);
    
    private Breakpoint _currentBreakpoint = Breakpoint.Sm;
    
    // ReSharper disable once MemberCanBeProtected.Global
    /// <summary>
    /// Gets the breakpoint calculated in the last measure pass.
    /// </summary>
    public Breakpoint CurrentBreakpoint => _currentBreakpoint;

    public BreakpointAwarePanel()
    {
        this.UpdatePseudeClasses();
    }
    
    protected override Size MeasureCore(Size availableSize)
    {
        var breakpointBefore = _currentBreakpoint;
        _currentBreakpoint = CalculateBreakpoint(availableSize.Width);
        
        if (_currentBreakpoint != breakpointBefore) { this.UpdatePseudeClasses(); }
        
        return base.MeasureCore(availableSize);
    }

    private void UpdatePseudeClasses()
    {
        this.PseudoClasses.Set(
            GetPseudoClassByBreakpoint(Breakpoint.Sm),
            _currentBreakpoint >= Breakpoint.Sm);
        this.PseudoClasses.Set(
            GetPseudoClassByBreakpoint(Breakpoint.Md),
            _currentBreakpoint >= Breakpoint.Md);
        this.PseudoClasses.Set(
            GetPseudoClassByBreakpoint(Breakpoint.Lg),
            _currentBreakpoint >= Breakpoint.Lg);
        this.PseudoClasses.Set(
            GetPseudoClassByBreakpoint(Breakpoint.Xl),
            _currentBreakpoint >= Breakpoint.Xl);
        this.PseudoClasses.Set(
            GetPseudoClassByBreakpoint(Breakpoint.Xxl),
            _currentBreakpoint >= Breakpoint.Xxl);
    }

    private static string GetPseudoClassByBreakpoint(Breakpoint breakpoint)
    {
        return breakpoint switch
        {
            // Xs is not relevant, it is the default
            Breakpoint.Sm => ":breakpoint-sm",
            Breakpoint.Md => ":breakpoint-md",
            Breakpoint.Lg => ":breakpoint-lg",
            Breakpoint.Xl => ":breakpoint-xl",
            Breakpoint.Xxl => ":breakpoint-xxl",
            _ => throw new ArgumentOutOfRangeException(nameof(breakpoint))
        };
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Calculates the breakpoint for the given width.
    /// </summary>
    protected Breakpoint CalculateBreakpoint(double width)
    {
        if (width >= this.BreakpointXxl)
        {
            return Breakpoint.Xxl;
        }

        if (width >= this.BreakpointXl)
        {
            return Breakpoint.Xl;
        }

        if (width >= this.BreakpointLg)
        {
            return Breakpoint.Lg;
        }

        if (width >= this.BreakpointMd)
        {
            return Breakpoint.Md;
        }

        if (width >= this.BreakpointSm)
        {
            return Breakpoint.Sm;
        }

        return Breakpoint.Xs;
    }
}