using Avalonia.Themes.Fluent;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection.Tests.Util;

public class TestThemeDetector : IOsThemeDetector
{
    public static TestThemeDetector Current { get; } = new (){ CurrentTheme = FluentThemeMode.Light };

    private Action<FluentThemeMode>? _setModeAction;

    public FluentThemeMode CurrentTheme
    {
        get;
        set;
    }

    private TestThemeDetector()
    {
        
    }

    public void TriggerThemeChange(FluentThemeMode newThemeMode)
    {
        this.CurrentTheme = newThemeMode;
        _setModeAction?.Invoke(newThemeMode);
    }
    
    /// <inheritdoc />
    public FluentThemeMode GetFluentThemeByCurrentTheme(FluentThemeMode defaultTheme)
    {
        return CurrentTheme;
    }

    /// <inheritdoc />
    public void ListenForThemeChange(FluentThemeMode defaultThemeMode, Action<FluentThemeMode> setModeAction)
    {
        _setModeAction = setModeAction;
    }
}