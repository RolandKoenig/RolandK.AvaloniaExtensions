using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.TestApp.Converters;

public class EnumerableToConcatenatedStringConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable enumerable)
        {
            return BindingOperations.DoNothing;
        }

        var resultBuilder = new StringBuilder(128);
        foreach (var actItem in enumerable)
        {
            if (resultBuilder.Length > 0)
            {
                resultBuilder.Append(',');
                resultBuilder.Append(' ');
            }
            
            if (actItem == null) { resultBuilder.Append("null"); }
            else { resultBuilder.Append(actItem.ToString()); }
        }
        return resultBuilder.ToString();
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Not supported
        return BindingOperations.DoNothing;
    }
}