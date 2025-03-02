using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a bool value in XAML.
/// </summary>
public class BoolExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public bool Value { get; set; }

    public BoolExtension()
    {
        
    }

    public BoolExtension(bool value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}