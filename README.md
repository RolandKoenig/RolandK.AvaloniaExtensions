# RolandK.AvaloniaExtensions <img src="assets/Logo_128.png" width="32" />
## Common Information
A .NET library which extends Avalonia with commonly used features like ViewServices, 
DependencyInjection and some Mvvm sugar

## Build
[![Continuous integration](https://github.com/RolandKoenig/RolandK.AvaloniaExtensions/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/RolandKoenig/RolandK.AvaloniaExtensions/actions/workflows/continuous-integration.yml)

## Feature overview
 - ViewServices over the popular Mvvm pattern by **not** providing an own Mvvm implementation
 - Some default ViewServices (FileDialogs, MessageBox)
 - DependencyInjection for Avalonia based on Microsft.Extensions.DependencyInjection
 - Notification on ViewModels when view is attaching and detaching
 - Automatically set FluentThemeMode at startup to OS theme (on Windows)

# Samples
Here you find examples to the features of RolandK.AvaloniaExtensions. Most of
these features work for themselves and are self-contained. They have no dependencies to
other features of RolandK.AvaloniaExtensions.

## DependencyInjection for Avalonia based on Microsft.Extensions.DependencyInjection
Add nuget package [RolandK.AvaloniaExtensions.DependencyInjection](https://www.nuget.org/packages/RolandK.AvaloniaExtensions.DependencyInjection)

Enable DependencyInjection by calling UseDependencyInjection on AppBuilder during
startup of your Avalonia application. This method registers the ServiceProvider as
a globally available resource on your Application object. You can find the key
of the resource within the constant DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY.

```csharp
using RolandK.AvaloniaExtensions.DependencyInjection;

public static class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            //...
            .UseDependencyInjection(services =>
            {
                // Services
                services.AddSingleton<ITestDataGenerator, BogusTestDataGenerator>();
                
                // ViewModels
                services.AddTransient<MainWindowViewModel>();
            });
}
```

Now you can inject ViewModels via the MarkupExtension CreateUsingDependencyInjection
in xaml namespace 'https://github.com/RolandK.AvaloniaExtensions'

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:ext="https://github.com/RolandK.AvaloniaExtensions"
        xmlns:local="clr-namespace:RolandK.AvaloniaExtensions.TestApp"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="RolandK.AvaloniaExtensions.TestApp.MainWindow"
        Title="{Binding Path=Title}"
        ExtendClientAreaToDecorationsHint="True"
        DataContext="{ext:CreateUsingDependencyInjection {x:Type local:MainWindowViewModel}}"
        d:DataContext="{x:Static local:MainWindowViewModel.DesignViewModel}">
    <!-- ... -->
</Window>
```

## Automatically set FluentThemeMode at startup to OS theme (on Windows)
Add nuget package [RolandK.AvaloniaExtensions.FluentThemeDetection](https://www.nuget.org/packages/RolandK.AvaloniaExtensions.FluentThemeDetection).

Ensure that you use a FluentTheme in App.axaml.
```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="RolandK.AvaloniaExtensions.TestApp.App">
    <Application.Styles>
        <FluentTheme Mode="Dark"/>
    </Application.Styles>
</Application>
```

Now you can call UseFluentThemeDetection from within your Program.cs.
This method will automatically search for FluentTheme in the Application's styles
and updates the Mode property.
```csharp
using RolandK.AvaloniaExtensions.FluentThemeDetection;

public static class Program
{
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            //...
            .UseFluentThemeDetection();
}
```

You can also call the extension method TrySetFluentThemeMode on your Application object
to set the currently active theme manually. This method doesn't need a call to UseFluentThemeDetection 
during startup.
```csharp
Application.Current.TrySetFluentThemeMode(FluentThemeMode.Light);
```