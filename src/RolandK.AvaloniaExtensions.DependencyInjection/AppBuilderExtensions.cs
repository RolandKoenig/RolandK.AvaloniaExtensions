using System;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.DependencyInjection;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Registers Microsoft.Extensions.DependencyInjection for this application.
    /// The <see cref="ServiceProvider"/> is added to the application's resources.
    ///
    /// This method can be called multiple times. Each time it gets the <see cref="IServiceCollection"/>
    /// instance from the previous call. 
    /// </summary>
    /// <param name="appBuilder"></param>
    /// <param name="registerServicesAction">Callback for registering application's services and ViewModels</param>
    public static AppBuilder UseDependencyInjection(this AppBuilder appBuilder, Action<IServiceCollection> registerServicesAction)
    {
        if (Design.IsDesignMode) { return appBuilder; }
        
        appBuilder.AfterSetup(x =>
        {
            if (x.Instance == null) { return; }
            
            // Get IServiceCollection object
            // We search for an existing one to be able to call UseDependencyInjection multiple times (needed for UI-Testing)
            IServiceCollection services;
            if (x.Instance.Resources.TryGetValue(
                    DependencyInjectionConstants.SERVICE_COLLECTION_RESOURCE_KEY,
                    out var existingServiceCollectionObj) 
                && existingServiceCollectionObj is IServiceCollection existingServiceCollection)
            {
                services = existingServiceCollection;
            }
            else
            {
                services = new ServiceCollection();
                x.Instance.Resources.Add(
                    DependencyInjectionConstants.SERVICE_COLLECTION_RESOURCE_KEY,
                    services);
            }
            
            // Update services
            registerServicesAction(services);
            
            // Create the IServiceProvider
            x.Instance.Resources[DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY] =
                services.BuildServiceProvider();
        });

        return appBuilder;
    }
}