using System;
using Avalonia.Interactivity;
using RolandK.AvaloniaExtensions.ExceptionHandling;
using RolandK.AvaloniaExtensions.Mvvm.Controls;

namespace RolandK.AvaloniaExtensions.TestApp;

public partial class MainWindow : MvvmWindow
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private async void OnMnu_ShowDummyException_ThisProcess_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            throw new InvalidOperationException("Dummy exception");
        }
        catch (Exception ex)
        {
            await GlobalErrorReporting.ShowGlobalExceptionDialogAsync(ex, this);
        }
    }

    private void OnMnu_ShowDummyException_OtherProcess_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            throw new InvalidOperationException("Dummy exception");
        }
        catch (Exception ex)
        {
            GlobalErrorReporting.TryShowBlockingGlobalExceptionDialogInAnotherProcess(
                ex,
                ".RKAvaloniaExtensions.TestApp",
                "RolandK.AvaloniaExtensions.TestApp.ExceptionViewer");
        }
    }

    private void OnMnu_SimulateUnhandledException_Click(object? sender, RoutedEventArgs e)
    {
        throw new InvalidOperationException("Dummy exception");
    }
}
