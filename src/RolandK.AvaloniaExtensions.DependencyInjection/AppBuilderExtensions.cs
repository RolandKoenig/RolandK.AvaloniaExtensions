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
    /// This method should only be called once
    /// </summary>
    /// <param name="appBuilder"></param>
    /// <param name="registerServicesAction">Callback for registering application's services and ViewModels</param>
    public static AppBuilder UseDependencyInjection(
        this AppBuilder appBuilder, 
        Action<IServiceCollection> registerServicesAction)
        => UseDependencyInjection(appBuilder, registerServicesAction, out _);

    /// <summary>
    /// Registers Microsoft.Extensions.DependencyInjection for this application.
    /// The <see cref="ServiceProvider"/> is added to the application's resources.
    ///
    /// This method should only be called once
    /// </summary>
    /// <param name="appBuilder"></param>
    /// <param name="registerServicesAction">Callback for registering application's services and ViewModels</param>
    /// <param name="serviceProvider">The generated <see cref="IServiceProvider"/> for further usage within application bootstrap</param>
    public static AppBuilder UseDependencyInjection(
        this AppBuilder appBuilder,
        Action<IServiceCollection> registerServicesAction, 
        out IServiceProvider serviceProvider)
    {
        if (Design.IsDesignMode)
        {
            serviceProvider = null!;
            return appBuilder;
        }
        
        var services = new ServiceCollection();
        registerServicesAction(services);

        serviceProvider = services.BuildServiceProvider();

        var serviceProviderCaptured = serviceProvider;
        appBuilder.AfterSetup(x =>
        {
            if (x.Instance == null) { return; }
            
            // ReSharper disable once CanSimplifyDictionaryLookupWithTryAdd
            //
            // Try to detect duplicate UseDependencyInjection calls
            if (x.Instance.Resources.ContainsKey(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY))
            {
                throw new DuplicateCallToUseDependencyInjectionException();
            }
            
            // Attach the ServiceProvider to the App's resources
            x.Instance.Resources[DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY] = serviceProviderCaptured;
        });

        return appBuilder;
    }
}