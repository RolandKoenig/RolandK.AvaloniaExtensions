using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class BoolValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public bool Value { get; set; }

    public BoolValueExtension()
    {
        
    }

    public BoolValueExtension(bool value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}