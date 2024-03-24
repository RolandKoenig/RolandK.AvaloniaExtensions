using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using RolandK.AvaloniaExtensions.ExceptionHandling.Data;

namespace RolandK.AvaloniaExtensions.ExceptionHandling;

public class ExceptionViewerApplication : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ExceptionInfo? exceptionInfo = null;
            try
            {
                var filePath = desktop!.Args![0];
            
                using var inStream = File.OpenRead(filePath);
                exceptionInfo = JsonSerializer.Deserialize<ExceptionInfo>(inStream);
            }
            catch (Exception)
            {
                // Nothing we can do here
            }
            
            if (exceptionInfo == null)
            {
                // We need to wait some time. Otherwise, an exception is thrown after Shutdown()
                Task.Delay(100).ContinueWith(_ =>
                {
                    Dispatcher.UIThread.Invoke(() =>
                    {
                        desktop.Shutdown();
                    });
                });
                base.OnFrameworkInitializationCompleted();
                return;
            }
            
            var dialog = new UnexpectedErrorDialog();
            dialog.DataContext = exceptionInfo;
            desktop.MainWindow = dialog;
        }

        base.OnFrameworkInitializationCompleted();
    }
}