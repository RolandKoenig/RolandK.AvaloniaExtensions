using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class IntValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public int Value { get; set; }

    public IntValueExtension()
    {
        
    }

    public IntValueExtension(int value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}