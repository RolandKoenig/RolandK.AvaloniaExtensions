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
Here you find examples to most of the features of RolandK.AvaloniaExtensions. All of
these features work for themselves and are self-contained. They have no dependencies to
other features of RolandK.AvaloniaExtensions.

## DependencyInjection for Avalonia based on Microsft.Extensions.DependencyInjection
First, add nuget package [RolandK.AvaloniaExtensions.DependencyInjection](https://www.nuget.org/packages/RolandK.AvaloniaExtensions.DependencyInjection)

Then enable DependencyInjection by calling UseDependencyInjection on AppBuilder during
startup of your Avalonia application. This method registers the ServiceProvider as
a globally available resource on your Application object.

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
First, add nuget package [RolandK.AvaloniaExtensions.FluentThemeDetection](https://www.nuget.org/packages/RolandK.AvaloniaExtensions.FluentThemeDetection).

Then ensure that you use a FluentTheme in App.axaml.
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
This method will automatically search for FluentTheme in die Application's styles
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