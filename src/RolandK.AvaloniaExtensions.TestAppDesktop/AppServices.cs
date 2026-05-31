using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.TestAppDesktop.Services;
using RolandK.AvaloniaExtensions.TestAppDesktop.Views;

namespace RolandK.AvaloniaExtensions.TestAppDesktop;

public static class AppServices
{
    public static void Configure(IServiceCollection services)
    {
        // Services
        services.AddSingleton<ITestDataGenerator, BogusTestDataGenerator>();
                
        // ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<DataTableViewModel>();
        services.AddTransient<ResponsiveTwoColumnViewModel>();
    }
}