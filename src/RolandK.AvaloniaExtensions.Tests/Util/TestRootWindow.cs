using Avalonia.Controls;
using Avalonia.Platform;
using NSubstitute;

namespace RolandK.AvaloniaExtensions.Tests.Util;

internal class TestRootWindow : Window
{
    public TestRootWindow()
        : base(Substitute.For<IWindowImpl>())
    {

    }

    public TestRootWindow(IControl initialChild)
        : this()
    {
        this.Content = initialChild;
    }
}