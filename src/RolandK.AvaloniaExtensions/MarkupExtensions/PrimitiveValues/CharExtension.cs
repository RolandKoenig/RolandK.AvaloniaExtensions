using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a char value in XAML.
/// </summary>
public class CharExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public char Value { get; set; }

    public CharExtension()
    {
        
    }

    public CharExtension(char value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}