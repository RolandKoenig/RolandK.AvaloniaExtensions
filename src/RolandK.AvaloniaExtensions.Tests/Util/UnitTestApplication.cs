using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.Threading;

namespace RolandK.AvaloniaExtensions.Tests.Util;

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

        await StartApplicationAsync();
        
        if (action != null)
        {
            await Dispatcher.UIThread.InvokeAsync(action);
        }
    }
    
    public static async Task RunInApplicationContextAsync(Func<Task> asyncAction)
    {
        if (Application.Current != null)
        {
            await Dispatcher.UIThread.InvokeAsync(asyncAction);
            return;
        }

        await StartApplicationAsync();
        
        await Dispatcher.UIThread.InvokeAsync(asyncAction);
    }
    
    public static async Task StopAsync()
    {
        var application = Application.Current;
        if (application == null) { return; }

        var appLifetime = application.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (appLifetime == null) { return; }

        await Dispatcher.UIThread.InvokeAsync(() => appLifetime.Shutdown());
    }
    
    private static async Task StartApplicationAsync()
    {
        var taskComplSource = new TaskCompletionSource();
        var uiThread = new Thread(_ =>
        {
            AppBuilder.Configure<UnitTestApplication>()
                .LogToTrace()
                .UseHeadless(new AvaloniaHeadlessPlatformOptions())
                .AfterSetup(_ => taskComplSource.SetResult())
                .StartWithClassicDesktopLifetime(Array.Empty<string>(), ShutdownMode.OnExplicitShutdown);
        });
        uiThread.Start();

        await taskComplSource.Task;
    }
}