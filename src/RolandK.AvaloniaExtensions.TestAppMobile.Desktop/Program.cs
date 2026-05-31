using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.TestAppMobile.Desktop;

public static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseDependencyInjection(
                services => services.AddAppServices(),
                out var serviceProvider)
#if DEBUG
            .WithDeveloperTools(options =>
            {
                options.AddMicrosoftLoggerObservable(
                    serviceProvider.GetRequiredService<ILoggerFactory>());
            })
#endif
            .WithInterFont()
            .LogToTrace();
}