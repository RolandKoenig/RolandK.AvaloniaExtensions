using Avalonia.Controls;

namespace RolandK.AvaloniaExtensions.Tests.Util;

internal class TestRootWindow : Window
{
    public TestRootWindow()
        : base()
    {

    }

    public TestRootWindow(Control initialChild)
        : this()
    {
        this.Content = initialChild;
    }

    public static TestRootWindow CreateAndShow(Control initialChild)
    {
        var window = new TestRootWindow(initialChild);
        window.Show();
        return window;
    }
}