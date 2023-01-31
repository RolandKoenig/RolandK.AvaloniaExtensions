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
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.HeaderMenuArea.Add(new TestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_HeaderMenuArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.HeaderMenuArea.Add(new TestControl());
            mainWindowFrame.HeaderMenuArea.Add(new AnotherTestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<AnotherTestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_CustomTitleArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.CustomTitleArea.Add(new TestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_CustomTitleArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.CustomTitleArea.Add(new TestControl());
            mainWindowFrame.CustomTitleArea.Add(new AnotherTestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<AnotherTestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_MainContentArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.MainContentArea.Add(new TestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_MainContentArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.MainContentArea.Add(new TestControl());
            mainWindowFrame.MainContentArea.Add(new AnotherTestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<AnotherTestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_FooterArea_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.FooterArea.Add(new TestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public Task Set_FooterArea_multiple_content()
    {
        return UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.FooterArea.Add(new TestControl());
            mainWindowFrame.FooterArea.Add(new AnotherTestControl());
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();

            // Assert
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<TestControl>());
            Assert.NotNull(mainWindow.FindLogicalDescendantOfType<AnotherTestControl>());
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Theory]
    [InlineData(MainWindowFrameStatus.Hidden, false)]
    [InlineData(MainWindowFrameStatus.NeutralGray, true)]
    [InlineData(MainWindowFrameStatus.NeutralBlue, true)]
    [InlineData(MainWindowFrameStatus.Green, true)]
    [InlineData(MainWindowFrameStatus.Red, true)]
    [InlineData(MainWindowFrameStatus.Yellow, true)]
    public async Task Set_Status(MainWindowFrameStatus statusToSet, bool expectedVisibility)
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindowFrame.Status = statusToSet;
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();
            
            // Assert
            var statusControl = TestUtil.TryFindLogicalDescendantWithName<IControl>(
                mainWindow, "CtrlStatusBar");
            Assert.NotNull(statusControl);
            Assert.Equal(expectedVisibility, statusControl.IsVisible);
            
            // Cleanup
            mainWindow.Close();
        });
    }

    [Fact]
    public async Task Attaching_to_Windows_sets_IsAttachedToWindow_to_true()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();
            var mainWindow = new Window();
            
            // Act
            mainWindow.Content = mainWindowFrame;
            mainWindow.Show();
            
            // Assert
            Assert.True(mainWindowFrame.IsAttachedToWindow);
            
            // Cleanup
            mainWindow.Close();
        });
    }
    
    [Fact]
    public async Task Attaching_to_Control_sets_IsAttachedToWindow_to_false()
    {
        await UnitTestApplication.RunInApplicationContextAsync(() =>
        {
            // Arrange
            var mainWindowFrame = new MainWindowFrame();

            // Act
            var testRoot = new TestRoot(mainWindowFrame);
            
            // Assert
            Assert.False(mainWindowFrame.IsAttachedToWindow);

            GC.KeepAlive(testRoot);
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