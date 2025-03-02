using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides an uint value in XAML.
/// </summary>
public class UIntExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public uint Value { get; set; }

    public UIntExtension()
    {
        
    }

    public UIntExtension(uint value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}