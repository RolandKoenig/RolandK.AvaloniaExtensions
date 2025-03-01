using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RolandK.AvaloniaExtensions.DependencyInjection.Markup;
using RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests;

public class DependencyInjectionTests
{
    public DependencyInjectionTests()
    {
        Application.Current!.Resources[DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY] = new ServiceCollection()
            .AddTransient<IDummyService, DummyServiceImpl>()
            .AddSingleton<MyDummyViewModel>()
            .BuildServiceProvider();
    }

    [AvaloniaFact]
    public void After_startup_check_IServiceProvider_in_resources()
    {
        // Assert
        var serviceProvider = Application.Current!.FindResource(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY);
        Assert.NotNull(serviceProvider);
        Assert.IsAssignableFrom<IServiceProvider>(serviceProvider);
    }
    
    [AvaloniaFact]
    public void After_startup_check_IServiceProvider_contains_MyDummyViewModel()
    {
        // Arrange
        var serviceProvider = (IServiceProvider)Application.Current!.FindResource(
            DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY)!;
            
        // Act
        var dummyViewModel = serviceProvider.GetService<MyDummyViewModel>();

        // Assert
        Assert.NotNull(dummyViewModel);
    }

    [AvaloniaFact]
    public void Create_ViewModel_by_CreateUsingDependencyInjectionExtension_after_attached_to_logical_tree()
    {
        // Arrange
        var myUserControl = new UserControl();
        var rootObjectProvider = Substitute.For<IRootObjectProvider>();
        rootObjectProvider.RootObject.Returns(myUserControl);

        var markupExtensionServiceProvider = Substitute.For<IServiceProvider>();
        markupExtensionServiceProvider.GetService(typeof(IRootObjectProvider)).Returns(rootObjectProvider);
            
        // Act
        var testWindow = new TestRootWindow(myUserControl);
        testWindow.Show();
            
        var attribute = new CreateUsingDependencyInjectionExtension(typeof(MyDummyViewModel));
        var viewModel = attribute.ProvideValue(markupExtensionServiceProvider);

        // Assert
        Assert.NotNull(viewModel);
        Assert.IsType<MyDummyViewModel>(viewModel);
            
        // Cleanup
        testWindow.Close();
    }
    
    [AvaloniaFact]
    public void Assign_ViewModel_by_CreateUsingDependencyInjectionExtension_after_attached_to_logical_tree()
    {
        // Arrange
        var myUserControl = new UserControl();
            
        var rootObjectProvider = Substitute.For<IRootObjectProvider>();
        rootObjectProvider.RootObject.Returns(myUserControl);

        var provideValueTarget = Substitute.For<IProvideValueTarget>();
        provideValueTarget.TargetObject.Returns(myUserControl);
        provideValueTarget.TargetProperty.Returns(StyledElement.DataContextProperty);

        var markupExtensionServiceProvider = Substitute.For<IServiceProvider>();
        markupExtensionServiceProvider.GetService(typeof(IRootObjectProvider)).Returns(rootObjectProvider);
        markupExtensionServiceProvider.GetService(typeof(IProvideValueTarget)).Returns(provideValueTarget);
            
        // Act
        var attribute = new CreateUsingDependencyInjectionExtension(typeof(MyDummyViewModel));
        var initialProvideValueResult = attribute.ProvideValue(markupExtensionServiceProvider);
            
        var testWindow = new TestRootWindow(myUserControl);
        testWindow.Show();

        var viewModel = myUserControl.DataContext;

        // Assert
        Assert.Null(initialProvideValueResult); // this one is null because myUserControl was not attached to logical tree
        Assert.NotNull(viewModel); // This one is set because the attribute automatically assigns the value after myUserControl is attached to logical tree 
        Assert.IsType<MyDummyViewModel>(viewModel);
            
        // Cleanup
        testWindow.Close();
    }

    [AvaloniaFact]
    public void Create_Control_and_get_ServiceProvider_from_it()
    {
        // Arrange
        var myUserControl = new UserControl();
            
        // Act
        var testWindow = new TestRootWindow(myUserControl);
        testWindow.Show();

        var serviceProvider = myUserControl.GetServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider);
        Assert.NotNull(serviceProvider.GetService(typeof(IDummyService)));
            
        // Cleanup
        testWindow.Close();
    }
    
    [AvaloniaFact]
    public void Get_ServiceProvider_from_Application()
    {
        // Act
        var serviceProvider = Application.Current?.GetServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider);
        Assert.NotNull(serviceProvider.GetService(typeof(IDummyService)));
    }
    
    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class MyDummyViewModel
    {
        
    }
    
    private interface IDummyService
    {
        
    }

    private class DummyServiceImpl : IDummyService
    {
        
    }
}