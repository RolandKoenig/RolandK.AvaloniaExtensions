using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(
    typeof(RolandK.AvaloniaExtensions.Tests.Util.TestAppBuilder))]

namespace RolandK.AvaloniaExtensions.Tests.Util;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}