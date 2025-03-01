using Avalonia.Controls;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

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
}