using Avalonia;
using Avalonia.Logging;
using Avalonia.Themes.Fluent;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection;

public static class ApplicationExtensions
{
    /// <summary>
    /// Tries to set the given <see cref="FluentThemeMode"/> on the current <see cref="Application"/> object.
    /// Returns false, if it was not possible to change it.
    /// </summary>
    /// <param name="application">Current <see cref="Application"/>, may be null.</param>
    /// <param name="themeMode">The <see cref="FluentThemeMode"/> to set.</param>
    public static bool TrySetFluentThemeMode(this Application? application, FluentThemeMode themeMode)
    {
        if (application == null) { return false; }

        var fluentTheme = application.TryGetFluentTheme();
        if (fluentTheme == null)
        {
            Logger.Sink?.Log(
                LogEventLevel.Error,
                typeof(AppBuilderExtensions).Namespace ?? "",
                null,
                "Unable to find FluentTheme object on current Application. Automated theme switch does not work");
            return false;
        }

        ((FluentTheme) fluentTheme).Mode = themeMode;
        return true;
    }

    public static FluentTheme? TryGetFluentTheme(this Application? application)
    {
        return application?.Styles.FirstOrDefault(x => x.GetType() == typeof(FluentTheme))
            as FluentTheme;
    }
}