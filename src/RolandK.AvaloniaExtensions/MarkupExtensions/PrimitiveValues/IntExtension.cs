using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides an int value in XAML.
/// </summary>
public class IntExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public int Value { get; set; }

    public IntExtension()
    {
        
    }

    public IntExtension(int value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}