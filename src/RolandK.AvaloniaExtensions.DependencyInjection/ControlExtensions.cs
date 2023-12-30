using Avalonia;
using Avalonia.Controls;

namespace RolandK.AvaloniaExtensions.DependencyInjection;

public static class ControlExtensions
{
    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> for this application.
    /// Returns null, if the <see cref="IServiceProvider"/> could not be found.
    /// </summary>
    public static IServiceProvider? TryGetServiceProvider(this StyledElement control)
    {
        if(control.TryFindResource(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY, out var resource) &&
           resource is IServiceProvider serviceProvider)
        {
            return serviceProvider;
        }
        return null;
    }

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> for this application.
    /// </summary>
    /// <exception cref="InvalidOperationException"><see cref="IServiceProvider"/> could not be found</exception>
    public static IServiceProvider GetServiceProvider(this StyledElement control)
    {
        var serviceProvider = TryGetServiceProvider(control);
        if (serviceProvider == null)
        {
            throw new InvalidOperationException(
                $"{nameof(IServiceProvider)} not found! Call to {nameof(AppBuilderExtensions.UseDependencyInjection)} in Program.cs may be missing...");
        }
        return serviceProvider;
    }
}
