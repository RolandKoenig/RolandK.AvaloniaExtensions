using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class SByteValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public sbyte Value { get; set; }

    public SByteValueExtension()
    {
        
    }

    public SByteValueExtension(sbyte value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}