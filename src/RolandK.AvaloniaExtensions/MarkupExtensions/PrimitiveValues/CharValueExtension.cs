using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class CharValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public char Value { get; set; }

    public CharValueExtension()
    {
        
    }

    public CharValueExtension(char value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}