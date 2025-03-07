using Microsoft.Extensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.DependencyInjection;

public static class DependencyInjectionConstants
{
    /// <summary>
    /// Original <see cref="ServiceCollection"/> the <see cref="IServiceProvider"/> is built from.
    /// We use this one internally to be able to override some services later and build a new service provider from that.
    /// </summary>
    public const string SERVICE_COLLECTION_RESOURCE_KEY =
        nameof(DependencyInjectionConstants) + "." + nameof(SERVICE_COLLECTION_RESOURCE_KEY);
    
    public const string SERVICE_PROVIDER_RESOURCE_KEY =
        nameof(DependencyInjectionConstants) + "." + nameof(SERVICE_PROVIDER_RESOURCE_KEY);
}