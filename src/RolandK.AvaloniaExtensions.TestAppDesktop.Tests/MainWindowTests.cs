using Avalonia.Headless.XUnit;

namespace RolandK.AvaloniaExtensions.TestAppDesktop.Tests;

public class MainWindowTests
{
    public MainWindowTests()
    {
        TestApp.Reset();
    }
    
    [AvaloniaFact]
    public void Test1()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}