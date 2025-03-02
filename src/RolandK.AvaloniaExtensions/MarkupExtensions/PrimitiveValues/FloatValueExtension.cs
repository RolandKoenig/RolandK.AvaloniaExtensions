using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class FloatValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public float Value { get; set; }

    public FloatValueExtension()
    {
        
    }

    public FloatValueExtension(float value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}