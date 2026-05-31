using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.AvaloniaExtensions.TestAppDesktop.Services;
using RolandK.AvaloniaExtensions.TestAppDesktop.Views;

namespace RolandK.AvaloniaExtensions.TestAppDesktop;

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
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<DataTableViewModel>();
        services.AddTransient<ResponsiveTwoColumnViewModel>();

        return services;
    }
}