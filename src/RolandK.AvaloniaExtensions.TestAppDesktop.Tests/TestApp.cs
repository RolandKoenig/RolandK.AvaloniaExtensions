using Avalonia;
using Avalonia.Headless;
using RolandK.AvaloniaExtensions.DependencyInjection;

[assembly: AvaloniaTestApplication(
    typeof(RolandK.AvaloniaExtensions.TestAppDesktop.Tests.TestApp))]

namespace RolandK.AvaloniaExtensions.TestAppDesktop.Tests;

public static class TestApp
{
    public static void Reset()
    {
      
    }
    
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .WithInterFont()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions())
        .UseDependencyInjection(services =>
        {
            services.AddAppServices();
            
            // Modify services
            // ...
        });
}