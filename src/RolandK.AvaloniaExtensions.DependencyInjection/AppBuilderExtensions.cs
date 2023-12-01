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
    /// </summary>
    /// <param name="appBuilder"></param>
    /// <param name="registerServicesAction">Callback for registering application's services and ViewModels</param>
    public static AppBuilder UseDependencyInjection(this AppBuilder appBuilder, Action<IServiceCollection> registerServicesAction)
    {
        if (Design.IsDesignMode) { return appBuilder; }
        
        appBuilder.AfterSetup(x =>
        {
            if (x == null) { return; }
            if (x.Instance == null) { return; }
            
            var services = new ServiceCollection();

            registerServicesAction(services);
            
            x.Instance.Resources.Add(
                DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY,
                services.BuildServiceProvider());
        });

        return appBuilder;
    }
}