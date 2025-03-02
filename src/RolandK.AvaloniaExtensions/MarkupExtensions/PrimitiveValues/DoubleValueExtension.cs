using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class DoubleValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public double Value { get; set; }

    public DoubleValueExtension()
    {
        
    }

    public DoubleValueExtension(double value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}