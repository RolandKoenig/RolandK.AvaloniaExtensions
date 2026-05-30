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
    
    public static AppBuilder BuildAvaloniaApp() => Program.BuildAvaloniaApp()
        .UseDependencyInjection(services =>
        {
            Reset();
            
            // Modify services
        })
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}