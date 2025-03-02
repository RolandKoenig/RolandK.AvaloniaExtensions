using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class UIntValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public uint Value { get; set; }

    public UIntValueExtension()
    {
        
    }

    public UIntValueExtension(uint value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}