using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides an ulong value in XAML.
/// </summary>
public class ULongExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public ulong Value { get; set; }

    public ULongExtension()
    {
        
    }

    public ULongExtension(ulong value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}