using System.Reflection;
using Avalonia;
using Avalonia.Headless.XUnit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests;

public class AppBuilderExtensionsTests
{
    [AvaloniaFact]
    public void Multiple_calls_to_UseDependencyInjection()
    {
        // Arrange
        var replacingServiceInstance = new ServiceClass1();
        var fakeAppBuilder = AppBuilder.Configure<App>()
            .UseDependencyInjection(services =>
            {
                services.AddSingleton<ServiceClass1>();
            })
            .UseDependencyInjection(services =>
            {
                services.Replace(ServiceDescriptor.Singleton(
                    typeof(ServiceClass1),
                    replacingServiceInstance));
                services.AddSingleton<ServiceClass2>();
            });

        var fakeApp = new App();
        SetAppInstance(fakeAppBuilder, fakeApp);

        // Act
        fakeAppBuilder.AfterSetupCallback.Invoke(fakeAppBuilder);
        
        // Assert
        var serviceProvider = fakeApp.GetServiceProvider();
        Assert.NotNull(serviceProvider);

        var instanceService1 = serviceProvider.GetRequiredService<ServiceClass1>();
        _ = serviceProvider.GetRequiredService<ServiceClass2>();
        Assert.Equal(instanceService1, replacingServiceInstance);
    }
    
    //-------------------------------------------------------------------------
    // Helpers
    
    private static void SetAppInstance(AppBuilder appBuilder, App app)
    {
        var appInstanceProperty = typeof(AppBuilder).GetProperty(
            "Instance",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        appInstanceProperty!.SetValue(appBuilder, app);
    }

    private class ServiceClass1
    {
        
    }
    
    private class ServiceClass2
    {
        
    }
}