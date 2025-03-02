using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class DecimalValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public decimal Value { get; set; }

    public DecimalValueExtension()
    {
        
    }

    public DecimalValueExtension(decimal value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}