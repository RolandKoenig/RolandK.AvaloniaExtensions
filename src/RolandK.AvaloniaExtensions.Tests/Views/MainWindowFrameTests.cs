using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Views;

namespace RolandK.AvaloniaExtensions.Tests.Views;

[Collection(nameof(ApplicationTestCollection))]
public class MainWindowFrameTests
{
    [Fact]
    public Task Set_HeaderMenuArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.HeaderMenuArea.Add(new TestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
        });
    }
    
    [Fact]
    public Task Set_HeaderMenuArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.HeaderMenuArea.Add(new TestControl());
            mainWindowFrame.HeaderMenuArea.Add(new AnotherTestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<AnotherTestControl>());
        });
    }
    
    [Fact]
    public Task Set_CustomTitleArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.CustomTitleArea.Add(new TestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
        });
    }
    
    [Fact]
    public Task Set_CustomTitleArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.CustomTitleArea.Add(new TestControl());
            mainWindowFrame.CustomTitleArea.Add(new AnotherTestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<AnotherTestControl>());
        });
    }
    
    [Fact]
    public Task Set_MainContentArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.MainContentArea.Add(new TestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
        });
    }
    
    [Fact]
    public Task Set_MainContentArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.MainContentArea.Add(new TestControl());
            mainWindowFrame.MainContentArea.Add(new AnotherTestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<AnotherTestControl>());
        });
    }
    
    [Fact]
    public Task Set_FooterArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.FooterArea.Add(new TestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
        });
    }
    
    [Fact]
    public Task Set_FooterArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            
            // Act
            mainWindowFrame.FooterArea.Add(new TestControl());
            mainWindowFrame.FooterArea.Add(new AnotherTestControl());
            var testRoot = new TestRoot(mainWindowFrame);

            // Assert
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(testRoot.FindLogicalDescendantOfType<AnotherTestControl>());
        });
    }
    
    //*************************************************************************
    //*************************************************************************
    //*************************************************************************
    private class TestControl : UserControl
    {
        
    }
    
    private class AnotherTestControl : UserControl
    {
        
    }
}