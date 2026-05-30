using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.TestAppDesktop.Tests;

public class UiTests
{
    [AvaloniaFact]
    public void Test1()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}