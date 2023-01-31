using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using NSubstitute;

namespace RolandK.AvaloniaExtensions.FluentThemeDetection.Tests.Util;

internal class UnitTestApplication : Application
{
    public static async Task RunInApplicationContextAsync(Action? action = null)
    {
        if (Application.Current != null)
        {
            if (action != null)
            {
                await Dispatcher.UIThread.InvokeAsync(action);
            }
            return;
        }

        var taskComplSource = new TaskCompletionSource();
        var uiThread = new Thread(_ =>
        {
            AppBuilder.Configure<UnitTestApplication>()
                .LogToTrace()
                .UseHeadless()
                .AfterSetup(appBuilder =>
                {
                    appBuilder.Instance.Styles.Add(
                        new FluentTheme(new Uri("https://github.com/RolandK.AvaloniaExtensions")));
                    taskComplSource.SetResult();
                })
                .StartWithClassicDesktopLifetime(Array.Empty<string>(), ShutdownMode.OnExplicitShutdown);
        });
        uiThread.Start();

        await taskComplSource.Task;

        if (action != null)
        {
            await Dispatcher.UIThread.InvokeAsync(action);
        }
    }
    
    public static async Task StopAsync()
    {
        var application = Application.Current;
        if (application == null) { return; }

        var appLifetime = application.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (appLifetime == null) { return; }

        await Dispatcher.UIThread.InvokeAsync(() => appLifetime.Shutdown());
    }
}