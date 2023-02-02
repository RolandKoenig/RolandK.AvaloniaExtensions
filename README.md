# RolandK.AvaloniaExtensions <img src="assets/Logo_128.png" width="32" />
## Common Information
A .NET library which extends Avalonia with commonly used features like ViewServices, 
DependencyInjection and some Mvvm sugar

## Build
[![Continuous integration](https://github.com/RolandKoenig/RolandK.AvaloniaExtensions/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/RolandKoenig/RolandK.AvaloniaExtensions/actions/workflows/continuous-integration.yml)

## Feature overview
 - [ViewServices over the popular Mvvm pattern by **not** providing an own Mvvm implementation](#viewservices-over-the-popular-mvvm-pattern)
 - [Some default ViewServices (FileDialogs, MessageBox)](#some-default-viewservices)
 - [Notification on ViewModels when view is attaching and detaching](#notification-on-viewmodels-when-view-is-attaching-and-detaching)
 - [DependencyInjection for Avalonia based on Microsft.Extensions.DependencyInjection](#dependencyinjection-for-avalonia-based-on-microsftextensionsdependencyinjection)
 - [Automatically set FluentThemeMode at startup to OS theme (on Windows)](#automatically-set-fluentthememode-at-startup-to-os-theme-on-windows)

# Samples
Here you find samples to the features of RolandK.AvaloniaExtensions. Most of
these features work for themselves and are self-contained. They have no dependencies to
other features of RolandK.AvaloniaExtensions. As Mvvm framework I use
[CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm) in all samples - but you are free to use another one. 
RolandK.AvaloniaExtensions has no dependencies on any Mvvm library and does not try to be an own implementation.

You can also take a look into the unittest projects. There you find
full examples for each provided feature.

## ViewServices over the popular Mvvm pattern
Add nuget package [RolandK.AvaloniaExtensions](https://www.nuget.org/packages/RolandK.AvaloniaExtensions).

ViewServices in RolandK.AvaloniaExtensions are interfaces provided by views (Windows, UserControls, etc.).
A view attaches itself to a view model using the IAttachableViewModel interface. Therefore, you have to 
implement this interface on your own view models. The following sample implementation is derived
from ObservableObject of CommunityToolkit.Mvvm.

```csharp
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.TestApp;

public class OwnViewModelBase : ObservableObject, IAttachableViewModel
{
    private object? _associatedView;
    
    /// <inheritdoc />
    public event EventHandler<CloseWindowRequestEventArgs>? CloseWindowRequest;
    
    /// <inheritdoc />
    public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

    /// <inheritdoc />
    public object? AssociatedView
    {
        get => _associatedView;
        set
        {
            if(_associatedView != value)
            {
                _associatedView = value;
                this.OnAssociatedViewChanged(_associatedView);
            }
        }
    }

    protected T? TryGetViewService<T>()
        where T : class
    {
        var requestViewServiceArgs = new ViewServiceRequestEventArgs(typeof(T));
        this.ViewServiceRequest?.Invoke(this, requestViewServiceArgs);
        return requestViewServiceArgs.ViewService as T;
    }
    
    protected T GetViewService<T>()
        where T : class
    {
        var viewService = this.TryGetViewService<T>();
        if (viewService == null)
        {
            throw new InvalidOperationException($"ViewService {typeof(T).FullName} not found!");
        }

        return viewService;
    }

    protected void CloseHostWindow(object? dialogResult = null)
    {
        if (this.CloseWindowRequest == null)
        {
            throw new InvalidOperationException("Unable to call Close on host window!");
        }
        
        this.CloseWindowRequest.Invoke(
            this, 
            new CloseWindowRequestEventArgs(dialogResult));
    }
    
    protected void OnAssociatedViewChanged(object? associatedView)
    {
        
    }
}
```

Now you can access ViewServices from within the view model by calling
GetViewService or TryGetViewService. The later does not throw an exception,
when the ViewService can not be found. 

In order for that to work, you also have to use one of the base classes MvvmWindow or MvvmUserControl on the
view side. They are responsible for attaching to the view model and detaching again, when
the view is closed. Be sure that you also derive from the correct base class in
the corresponding code behind.

```xml
<ext:MvvmWindow xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:ext="https://github.com/RolandK.AvaloniaExtensions"
        xmlns:local="clr-namespace:RolandK.AvaloniaExtensions.TestApp"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="RolandK.AvaloniaExtensions.TestApp.MainWindow">
</ext:MvvmWindow>
```

Register own ViewServices using the ViewServices property of MvvmWindow or
MvvmUserControl. 

The following code snipped is a command implementation within the view model.
It uses the ViewServices IOpenFileViewServices and IMessageBoxService. Both of
them are provided by default by RolandK.AvaloniaExtensions. 

```csharp
[RelayCommand]
public async Task OpenFileAsync()
{
    var srvOpenFile = this.GetViewService<IOpenFileViewService>();
    var srvMessageBox = this.GetViewService<IMessageBoxViewService>();
    
    var selectedFile =  await srvOpenFile.ShowOpenFileDialogAsync(
        Array.Empty<FileDialogFilter>(),
        "Open file");
    if (string.IsNullOrEmpty(selectedFile)) { return; }
    
    await srvMessageBox.ShowAsync(
        "Open file",
        $"File {selectedFile} selected", MessageBoxButtons.Ok);
}
```

## Some default ViewServices
RolandK.AvaloniaExtensions provides the following default ViewServices:
 - IMessageBoxViewService
 - IOpenDirectoryViewService
 - IOpenFileViewService
 - ISaveFileViewService

## Notification on ViewModels when view is attaching and detaching
As some kind of extension to the provided ViewService feature, the IAttachableViewModel
interface can be used to react on attaching / detaching of the view from within the view model.
You can use this for example to start and stop a timer in the view model.

The following code snipped shows how to write a OnAssociatedViewChanged method
on a view model base class.

```csharp
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using RolandK.AvaloniaExtensions.Mvvm;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.TestApp;

public class OwnViewModelBase : ObservableObject, IAttachableViewModel
{
    private object? _associatedView;
    
    //. ..
    
    /// <inheritdoc />
    public object? AssociatedView
    {
        get => _associatedView;
        set
        {
            if(_associatedView != value)
            {
                _associatedView = value;
                this.OnAssociatedViewChanged(_associatedView);
            }
        }
    }
    
    // ...
    
    protected void OnAssociatedViewChanged(object? associatedView)
    {
        
    }
}
```

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