namespace RolandK.AvaloniaExtensions.DependencyInjection;

public static class DependencyInjectionConstants
{
    /// <summary>
    /// We store <see cref="IServiceProvider"/> in the App's resource to be able to access it on every view.
    /// </summary>
    public const string SERVICE_PROVIDER_RESOURCE_KEY =
        nameof(DependencyInjectionConstants) + "." + nameof(SERVICE_PROVIDER_RESOURCE_KEY);
}