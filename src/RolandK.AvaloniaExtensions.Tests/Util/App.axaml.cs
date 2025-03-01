using Avalonia;
using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.Tests.Util;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}