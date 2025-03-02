using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

public class NIntValueExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public nint Value { get; set; }

    public NIntValueExtension()
    {
        
    }

    public NIntValueExtension(nint value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}