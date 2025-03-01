using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(
    typeof(RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util.TestAppBuilder))]

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseDependencyInjection(DependencyInjectionTests.AddServicesForUnitTests)
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}