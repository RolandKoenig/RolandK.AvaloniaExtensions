using Avalonia;
using Avalonia.Themes.Fluent;
using RolandK.AvaloniaExtensions.FluentThemeDetection.Tests.Util;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection.Tests;

[Collection(nameof(ApplicationTestCollection))]
public class ApplicationExtensionsTests
{
    [Fact]
    public Task Set_FluentTheme_mode_to_Light()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Act
            Application.Current.TrySetFluentThemeMode(FluentThemeMode.Light);
            
            // Assert
            var fluentTheme = Application.Current.TryGetFluentTheme();
            Assert.NotNull(fluentTheme);
            Assert.Equal(FluentThemeMode.Light, fluentTheme.Mode);
        });
    }
    
    [Fact]
    public Task Set_FluentTheme_mode_to_Dark()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Act
            Application.Current.TrySetFluentThemeMode(FluentThemeMode.Dark);
            
            // Assert
            var fluentTheme = Application.Current.TryGetFluentTheme();
            Assert.NotNull(fluentTheme);
            Assert.Equal(FluentThemeMode.Dark, fluentTheme.Mode);
        });
    }
}