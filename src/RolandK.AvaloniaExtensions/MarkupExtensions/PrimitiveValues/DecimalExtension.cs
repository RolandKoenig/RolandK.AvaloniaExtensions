using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a decimal value in XAML.
/// </summary>
public class DecimalExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public decimal Value { get; set; }

    public DecimalExtension()
    {
        
    }

    public DecimalExtension(decimal value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}