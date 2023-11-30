using Avalonia.Controls;
using Avalonia.Platform;
using NSubstitute;

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