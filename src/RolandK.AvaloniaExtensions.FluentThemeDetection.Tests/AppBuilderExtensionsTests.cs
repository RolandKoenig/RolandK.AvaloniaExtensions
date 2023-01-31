using Avalonia;
using Avalonia.Themes.Fluent;
using RolandK.AvaloniaExtensions.FluentThemeDetection.Tests.Util;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection.Tests;

[Collection(nameof(ApplicationTestCollection))]
public class AppBuilderExtensionsTests
{
    [Theory]
    [InlineData(FluentThemeMode.Light)]
    [InlineData(FluentThemeMode.Dark)]
    public async Task Switch_to_light_theme_by_ThemeDetector(FluentThemeMode themeToSet)
    {
        // Act
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            TestThemeDetector.Current.TriggerThemeChange(themeToSet);
        });
        
        // Assert
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            var fluentTheme = Application.Current.TryGetFluentTheme();
            Assert.NotNull(fluentTheme);
            Assert.Equal(themeToSet, fluentTheme.Mode);
        });
    }
}