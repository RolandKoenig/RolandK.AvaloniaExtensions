using System.Reflection;
using System.Reflection.Emit;

namespace RolandK.AvaloniaExtensions.Tests;

public class AvaloniaExtensionsConventionsTests
{
    [Theory]
    
    // Valid cases
    [InlineData("MainWindow", "MainWindowViewModel", true)]
    [InlineData("mainwindow", "mainwindowviewmodel", true)] // Different casing
    [InlineData("MapView", "MapViewModel", true)]
    [InlineData("mapview", "mapviewmodel", true)]
    [InlineData("Map", "MapViewModel", true)]
    [InlineData("map", "mapviewmodel", true)]
    [InlineData("View", "ViewViewModel", true)]
    
    // Invalid cases
    [InlineData("View", "ViewModel", false)]
    [InlineData("MapControl", "MapViewModel", false)]
    public void IsViewForViewModelConvention(string viewTypeName, string viewModelTypeName, bool expectedResult)
    {
        // Arrange
        var viewType = this.CreateDynamicType(viewTypeName);
        var viewModelType = this.CreateDynamicType(viewModelTypeName);
        
        // Act
        var result = AvaloniaExtensionsConventions.IsViewForViewModelFunc!.Invoke(viewType, viewModelType);
        
        // Assert
        Assert.Equal(expectedResult, result);
    }

    private Type CreateDynamicType(string typeName)
    {
        var assemblyName = new AssemblyName($"DynamicAssembly-{Guid.NewGuid()}");
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
        
        var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public);
        
        return typeBuilder.CreateType();
    }
}