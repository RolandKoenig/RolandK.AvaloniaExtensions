using Avalonia;
using Avalonia.Headless;
using Microsoft.Extensions.DependencyInjection;

[assembly: AvaloniaTestApplication(
    typeof(RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util.TestAppBuilder))]

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseDependencyInjection(services =>
        {
            services.AddTransient<IDummyService, DummyService>();
        })
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}