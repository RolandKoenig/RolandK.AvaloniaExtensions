using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.AvaloniaExtensions.TestAppMobile.Services;
using RolandK.AvaloniaExtensions.TestAppMobile.Views;

namespace RolandK.AvaloniaExtensions.TestAppMobile;

public static class AppServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // Common infrastructure
        services.AddLogging(builder => 
            builder.AddDebug());
        
        // Services
        services.AddSingleton<ITestDataGenerator, BogusTestDataGenerator>();

        // ViewModels
        services.AddTransient<MainViewModel>();
        
        return services;
    }
}