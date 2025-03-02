using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a byte value in XAML.
/// </summary>
public class ByteExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public byte Value { get; set; }

    public ByteExtension()
    {
        
    }

    public ByteExtension(byte value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}