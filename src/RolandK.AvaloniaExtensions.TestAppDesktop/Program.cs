using System;
using Avalonia;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;
using RolandK.AvaloniaExtensions.ExceptionHandling;

namespace RolandK.AvaloniaExtensions.TestAppDesktop;

public static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            GlobalErrorReporting.TryShowBlockingGlobalExceptionDialogInAnotherProcess(
                ex,
                ".RKAvaloniaExtensions.TestAppDesktop",
                "RolandK.AvaloniaExtensions.TestAppDesktop.ExceptionViewer");
            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
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
            .LogToTrace()
            .UsePlatformDetect();
}