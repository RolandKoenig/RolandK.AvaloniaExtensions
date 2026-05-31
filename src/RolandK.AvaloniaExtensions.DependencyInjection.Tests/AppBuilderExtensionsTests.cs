using System.Reflection;
using Avalonia;
using Avalonia.Headless.XUnit;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests;

public class AppBuilderExtensionsTests
{
    [AvaloniaFact]
    public void Multiple_calls_to_UseDependencyInjection_is_detected()
    {
        // Arrange
        var fakeAppBuilder = AppBuilder.Configure<App>()
            .UseDependencyInjection(services =>
            {
                services.AddSingleton<ServiceClass1>();
            })
            .UseDependencyInjection(services =>
            {
                services.AddSingleton<ServiceClass2>();
            });

        var fakeApp = new App();
        SetAppInstance(fakeAppBuilder, fakeApp);

        // Act / Assert
        Assert.Throws<DuplicateCallToUseDependencyInjectionException>(() =>
            fakeAppBuilder.AfterSetupCallback.Invoke(fakeAppBuilder));
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