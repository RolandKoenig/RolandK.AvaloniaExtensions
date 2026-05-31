using Android.App;
using Android.Runtime;
using Avalonia;
using Avalonia.Android;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace RolandK.AvaloniaExtensions.TestAppMobile.Android
{
    [Application]
    public class Application : AvaloniaAndroidApplication<App>
    {
        protected Application(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

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
}