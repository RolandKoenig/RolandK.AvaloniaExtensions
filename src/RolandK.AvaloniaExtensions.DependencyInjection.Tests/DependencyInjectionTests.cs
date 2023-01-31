using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RolandK.AvaloniaExtensions.DependencyInjection.Tests.Util;

namespace RolandK.AvaloniaExtensions.DependencyInjection.Tests;

[Collection(nameof(ApplicationTestCollection))]
public class DependencyInjectionTests
{
    /// <summary>
    /// Setup services for automated tests.
    /// </summary>
    internal static void AddServicesForUnitTests(IServiceCollection services)
    {
        // Services
        services.AddTransient<IDummyService, DummyServiceImpl>();
        
        // ViewModels
        services.AddSingleton<MyDummyViewModel>();
    }

    [Fact]
    public Task After_startup_check_IServiceProvider_in_resources()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Assert
            var serviceProvider = Application.Current!.FindResource(DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY);
            Assert.NotNull(serviceProvider);
            Assert.IsAssignableFrom<IServiceProvider>(serviceProvider);
        });
    }
    
    [Fact]
    public Task After_startup_check_IServiceProvider_contains_MyDummyViewModel()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var serviceProvider = (IServiceProvider)Application.Current!.FindResource(
                DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY)!;
            
            // Act
            var dummyViewModel = serviceProvider.GetService<MyDummyViewModel>();

            // Assert
            Assert.NotNull(dummyViewModel);
        });
    }

    [Fact]
    public Task Create_ViewModel_by_CreateUsingDependencyInjectionExtension_after_attached_to_logical_tree()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
        });
    }
    
    [Fact]
    public Task Assign_ViewModel_by_CreateUsingDependencyInjectionExtension_after_attached_to_logical_tree()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
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
        });
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