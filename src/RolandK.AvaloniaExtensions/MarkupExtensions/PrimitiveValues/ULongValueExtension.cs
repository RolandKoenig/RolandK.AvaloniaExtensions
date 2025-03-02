using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class ULongValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public ulong Value { get; set; }

    public ULongValueExtension()
    {
        
    }

    public ULongValueExtension(ulong value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}