using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class ByteValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public byte Value { get; set; }

    public ByteValueExtension()
    {
        
    }

    public ByteValueExtension(byte value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}