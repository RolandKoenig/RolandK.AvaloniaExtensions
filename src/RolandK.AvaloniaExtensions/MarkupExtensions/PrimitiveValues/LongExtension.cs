using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a long value in XAML.
/// </summary>
public class LongExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public long Value { get; set; }

    public LongExtension()
    {
        
    }

    public LongExtension(long value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}