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
## Automatically set FluentThemeMode to OS theme at startup
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