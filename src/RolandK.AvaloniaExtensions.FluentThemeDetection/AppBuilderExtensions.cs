using System;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Themes.Fluent;
using RolandK.AvaloniaExtensions.FluentThemeDetection.Windows;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Attaches to system events to detect theme changes.
    /// Updates current <see cref="FluentTheme"/> to reflect current system theme.
    /// </summary>
    /// <param name="appBuilder"></param>
    /// <param name="setThemeAction">A custom action to set <see cref="FluentThemeMode"/> on the <see cref="FluentTheme"/> of this application.</param>
    /// <returns></returns>
    public static AppBuilder UseFluentThemeDetection(
        this AppBuilder appBuilder, 
        Action<FluentThemeMode>? setThemeAction = null)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return appBuilder; }

        appBuilder.AfterSetup(_ =>
        {
            // This call prevents warnings from the compiler
            // Currently only windows is supported
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { return; }

            var initialThemeMode = WindowsThemeDetector.GetFluentThemeByCurrentWindowsTheme();
            if (setThemeAction != null) { setThemeAction(initialThemeMode); }
            else { Application.Current.TrySetFluentThemeMode(initialThemeMode); }

            var syncContext = SynchronizationContext.Current;
            if (syncContext == null)
            {
                Logger.Sink?.Log(
                    LogEventLevel.Error,
                    typeof(AppBuilderExtensions).Namespace ?? "",
                    null,
                    "Unable to get SynchronizationContext from UI thread. Automated theme switch will not work");
                return;
            }

            WindowsThemeDetector.ListenForThemeChangeEvent(fluentThemeMode =>
            {
                syncContext.Post(
                    _ =>
                    {
                        if (setThemeAction != null) { setThemeAction(fluentThemeMode); }
                        else
                        {
                            Application.Current.TrySetFluentThemeMode(fluentThemeMode);
                        }
                    },
                    null);
            });
        });

        return appBuilder;
    }
}
