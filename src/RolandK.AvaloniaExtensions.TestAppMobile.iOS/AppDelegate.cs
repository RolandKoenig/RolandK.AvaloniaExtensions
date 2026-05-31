using Foundation;
using Avalonia;
using Avalonia.iOS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.TestAppMobile.iOS;

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
[Register("AppDelegate")]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public partial class AppDelegate : AvaloniaAppDelegate<App>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .UseDependencyInjection(
                services => services.AddAppServices(),
                out var serviceProvider)
#if DEBUG
            .WithDeveloperTools(options =>
            {
                options.AddMicrosoftLoggerObservable(
                    serviceProvider.GetRequiredService<ILoggerFactory>());
            })
#endif
            .WithInterFont();
    }
}