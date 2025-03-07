using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection.Markup;
using RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests.Markup;

public class MarkupExtensionWithDependencyInjectionFakeExtension : MarkupExtensionWithDependencyInjection
{
    /// <inheritdoc />
    protected override object ProvideDefaultValue(IServiceProvider xamlServiceProvider)
    {
        return "";
    }

    /// <inheritdoc />
    protected override object ProvideValue(IServiceProvider xamlServiceProvider, IServiceProvider appServiceProvider)
    {
        var dummyService = appServiceProvider.GetRequiredService<IDummyService>();
        return dummyService.GetSomeDummyValue();
    }
}