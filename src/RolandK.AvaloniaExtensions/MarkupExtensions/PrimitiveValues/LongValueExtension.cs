using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class LongValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public long Value { get; set; }

    public LongValueExtension()
    {
        
    }

    public LongValueExtension(long value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}