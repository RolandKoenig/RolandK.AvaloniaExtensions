using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class UShortValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public ushort Value { get; set; }

    public UShortValueExtension()
    {
        
    }

    public UShortValueExtension(ushort value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}