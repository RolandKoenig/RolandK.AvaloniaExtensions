using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.MarkupExtensions.PrimitiveValues;

/// <summary>
/// Provides a double value in XAML.
/// </summary>
public class DoubleExtension : MarkupExtension
{
    [ConstructorArgument("value")]
    public double Value { get; set; }

    public DoubleExtension()
    {
        
    }

    public DoubleExtension(double value)
    {
        this.Value = value;
    }
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}