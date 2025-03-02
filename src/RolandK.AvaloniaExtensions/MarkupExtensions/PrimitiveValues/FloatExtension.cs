using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a float value in XAML.
/// </summary>
public class FloatExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public float Value { get; set; }

    public FloatExtension()
    {
        
    }

    public FloatExtension(float value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}