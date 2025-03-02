using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a sbyte value in XAML.
/// </summary>
public class SByteExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public sbyte Value { get; set; }

    public SByteExtension()
    {
        
    }

    public SByteExtension(sbyte value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}