using Avalonia;
using Avalonia.Headless;
using RolandK.AvaloniaExtensions.DependencyInjection;

[assembly: AvaloniaTestApplication(
    typeof(RolandK.AvaloniaExtensions.TestAppMobile.Tests.TestApp))]

namespace RolandK.AvaloniaExtensions.TestAppMobile.Tests;

public static class TestApp
{
    public static void Reset()
    {
        
    }
    
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .WithInterFont()
        .UseDependencyInjection(services =>
        {
            AppServices.Configure(services);
            
            // Modify services
            // ...
        })
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}