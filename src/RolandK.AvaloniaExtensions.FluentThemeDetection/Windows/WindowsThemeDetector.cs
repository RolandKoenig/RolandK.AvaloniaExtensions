using System;
using System.Globalization;
using System.Management;
using System.Runtime.Versioning;
using System.Security.Principal;
using Avalonia.Logging;
using Avalonia.Themes.Fluent;
using Microsoft.Win32;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection.Windows;

/// <summary>
/// Checks for currently configured theme in windows.
/// See: https://engy.us/blog/2018/10/20/dark-theme-in-wpf/
/// </summary>
internal static class WindowsThemeDetector
{
    private const string REGISTRY_KEY_PATH = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string REGISTRY_VALUE_NAME = "AppsUseLightTheme";

    [SupportedOSPlatform("windows")]
    public static void ListenForThemeChangeEvent(Action<FluentThemeMode> setWindowsThemeAction)
    {
        var currentUser = WindowsIdentity.GetCurrent();
        if (currentUser.User == null) { return; }

        string query = string.Format(
            CultureInfo.InvariantCulture,
            @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
            currentUser.User.Value,
            REGISTRY_KEY_PATH.Replace(@"\", @"\\"),
            REGISTRY_VALUE_NAME);
        try
        {
            var watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += (_, _) => setWindowsThemeAction(GetFluentThemeByCurrentWindowsTheme());

            // Start listening for events
            watcher.Start();
        }
        catch (Exception)
        {
            // This can fail on Windows 7
        }
    }

    [SupportedOSPlatform("windows")]
    public static FluentThemeMode GetFluentThemeByCurrentWindowsTheme(FluentThemeMode defaultTheme = FluentThemeMode.Light)
    {
        var subKey = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH);
        if (subKey == null)
        {
            Logger.Sink?.Log(
                LogEventLevel.Error,
                nameof(WindowsThemeDetector),
                null,
                "Unable to get registry key {RegistryKey}. Returning default theme {Theme}",
                REGISTRY_KEY_PATH, defaultTheme);
            return defaultTheme;
        }
        try
        {
            var registryValueObject = subKey.GetValue(REGISTRY_VALUE_NAME);
            if (registryValueObject == null)
            {
                Logger.Sink?.Log(
                    LogEventLevel.Error,
                    nameof(WindowsThemeDetector),
                    null,
                    "Unable to get registry key value (key: {RegistryKey}, value: {RegistryValueName}). Returning default theme {Theme}",
                    REGISTRY_KEY_PATH, REGISTRY_VALUE_NAME, defaultTheme);
                return defaultTheme;
            }

            var registryValue = (int)registryValueObject;
            var windowsTheme = registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
            return GetFluentThemeByWindowsTheme(windowsTheme);
        }
        finally
        {
            subKey.Dispose();
        }
    }

    private static FluentThemeMode GetFluentThemeByWindowsTheme(WindowsTheme windowsTheme)
    {
        switch (windowsTheme)
        {
            case WindowsTheme.Light:
                return FluentThemeMode.Light;

            case WindowsTheme.Dark:
            default:
                return FluentThemeMode.Dark;
        }
    }
}