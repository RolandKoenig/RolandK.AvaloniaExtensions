using Avalonia.Markup.Xaml;
using RolandK.AvaloniaExtensions.ExceptionHandling;

namespace RolandK.AvaloniaExtensions.TestAppDesktop.ExceptionViewer;

public partial class App : ExceptionViewerApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}