using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a nint value in XAML.
/// </summary>
public class NIntExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public nint Value { get; set; }

    public NIntExtension()
    {
        
    }

    public NIntExtension(nint value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}