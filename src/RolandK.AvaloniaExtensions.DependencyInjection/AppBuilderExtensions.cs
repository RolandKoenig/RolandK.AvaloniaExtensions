using System;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.DependencyInjection;

public static class AppBuilderExtensions
{
    public static AppBuilder UseDependencyInjection(this AppBuilder appBuilder, Action<IServiceCollection> registerServicesAction)
    {
        if (Design.IsDesignMode) { return appBuilder; }
        
        appBuilder.AfterSetup(x =>
        {
            var services = new ServiceCollection();

            registerServicesAction(services);
            
            x.Instance.Resources.Add(
                DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY,
                services.BuildServiceProvider());
        });

        return appBuilder;
    }
}