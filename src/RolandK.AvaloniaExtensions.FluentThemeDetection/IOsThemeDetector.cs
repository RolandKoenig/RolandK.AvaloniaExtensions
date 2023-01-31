using Avalonia.Themes.Fluent;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection;

public interface IOsThemeDetector
{
    /// <summary>
    /// Get the currently configured theme.
    /// </summary>
    /// <param name="defaultTheme">The default theme, if OS theme has no match to Avalonia themes.</param>
    FluentThemeMode GetFluentThemeByCurrentTheme(FluentThemeMode defaultTheme);

    /// <summary>
    /// Listens for theme changes on the OS.
    /// </summary>
    /// <param name="defaultThemeMode">The default theme, if OS theme has no match to Avalonia themes.</param>
    /// <param name="setModeAction">This action trigger after each change of the OS theme.</param>
    void ListenForThemeChange(FluentThemeMode defaultThemeMode, Action<FluentThemeMode> setModeAction);
}