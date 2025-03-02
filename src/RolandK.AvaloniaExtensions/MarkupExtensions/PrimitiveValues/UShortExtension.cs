using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides an ushort value in XAML.
/// </summary>
public class UShortExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public ushort Value { get; set; }

    public UShortExtension()
    {
        
    }

    public UShortExtension(ushort value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}