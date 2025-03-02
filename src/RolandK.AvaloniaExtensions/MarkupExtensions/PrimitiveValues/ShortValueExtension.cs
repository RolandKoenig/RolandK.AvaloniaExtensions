using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class ShortValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public short Value { get; set; }

    public ShortValueExtension()
    {
        
    }

    public ShortValueExtension(short value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}