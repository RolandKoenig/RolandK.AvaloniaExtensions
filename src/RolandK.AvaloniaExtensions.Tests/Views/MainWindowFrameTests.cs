using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Tests.Util;
using RolandK.AvaloniaExtensions.Controls;

namespace RolandK.AvaloniaExtensions.Tests.Views;

public class MainWindowFrameTests
{
    [AvaloniaFact]
    public void Set_HeaderMenuArea_content()
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
    }
    
    [AvaloniaFact]
    public void Set_HeaderMenuArea_multiple_content()
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
    }
    
    [AvaloniaFact]
    public void Set_CustomTitleArea_content()
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
    }
    
    [AvaloniaFact]
    public void Set_CustomTitleArea_multiple_content()
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
    }
    
    [AvaloniaFact]
    public void Set_MainContentArea_content()
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
    }
    
    [AvaloniaFact]
    public void Set_MainContentArea_multiple_content()
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
    }
    
    [AvaloniaFact]
    public void Set_FooterArea_content()
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
    }
    
    [AvaloniaFact]
    public void Set_FooterArea_multiple_content()
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
    }
    
    [AvaloniaTheory]
    [InlineData(MainWindowFrameStatus.Hidden, false)]
    [InlineData(MainWindowFrameStatus.NeutralGray, true)]
    [InlineData(MainWindowFrameStatus.NeutralBlue, true)]
    [InlineData(MainWindowFrameStatus.Green, true)]
    [InlineData(MainWindowFrameStatus.Red, true)]
    [InlineData(MainWindowFrameStatus.Yellow, true)]
    public void Set_Status(MainWindowFrameStatus statusToSet, bool expectedVisibility)
    {
        // Arrange
        var mainWindowFrame = new MainWindowFrame();
        var mainWindow = new Window();
            
        // Act
        mainWindowFrame.Status = statusToSet;
        mainWindow.Content = mainWindowFrame;
        mainWindow.Show();
            
        // Assert
        var statusControl = TestUtil.TryFindLogicalDescendantWithName<Control>(
            mainWindow, "CtrlStatusBar");
        Assert.NotNull(statusControl);
        Assert.Equal(expectedVisibility, statusControl.IsVisible);
            
        // Cleanup
        mainWindow.Close();
    }

    [AvaloniaFact]
    public void Attaching_to_Windows_sets_IsAttachedToWindow_to_true()
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
    }
    
    [AvaloniaFact]
    public void Attaching_to_Control_sets_IsAttachedToWindow_to_false()
    {
        // Arrange
        var mainWindowFrame = new MainWindowFrame();

        // Act
        var parentControl = new Panel();
        parentControl.Children.Add(mainWindowFrame);
            
        // Assert
        Assert.False(mainWindowFrame.IsAttachedToWindow);

        GC.KeepAlive(parentControl);
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