using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.Controls;
using RolandK.AvaloniaExtensions.ViewServices.FileDialogs;
using RolandK.AvaloniaExtensions.ViewServices.MessageBox;

namespace RolandK.AvaloniaExtensions.ViewServices;

internal static class DefaultViewServices
{
    public static object? TryGetDefaultViewService(Control host, Type viewServiceType)
    {
        if (viewServiceType == typeof(IOpenFileViewService))
        {
            var parentWindow = host.FindLogicalAncestorOfType<Window>(true);
            if (parentWindow == null) { return null; }
            
            return new OpenFileDialogService(parentWindow);
        }
        
        if (viewServiceType == typeof(ISaveFileViewService))
        {
            var parentWindow = host.FindLogicalAncestorOfType<Window>(true);
            if (parentWindow == null) { return null; }

            return new SaveFileDialogService(parentWindow);
        }
        
        if (viewServiceType == typeof(IMessageBoxViewService))
        {
            var dlgHostControl = TryFindDialogHostControl(host);
            if (dlgHostControl == null) { return null; }
            
            return new MessageBoxViewControlService(dlgHostControl);
        }

        if (viewServiceType == typeof(IOpenDirectoryViewService))
        {
            var parentWindow = host.FindLogicalAncestorOfType<Window>(true);
            if (parentWindow == null) { return null; }

            return new OpenDirectoryDialogService(parentWindow);
        }
        
        return null;
    }

    private static DialogHostControl? TryFindDialogHostControl(Control host)
    {
        // Method 1: Find ancestor
        var dlgHostControl = host.FindLogicalAncestorOfType<DialogHostControl>(true);
        
        // Method 2: Find over MainWindowFrame as ancestor
        if (dlgHostControl == null)
        {
            var mainWindowFrame = host.FindLogicalAncestorOfType<MainWindowFrame>(true);
            dlgHostControl = mainWindowFrame?.Overlay;
        }
        
        // Method 3: Find over Window as ancestor
        if (dlgHostControl == null)
        {
            var mainWindow = host.FindLogicalAncestorOfType<Window>(true);
            if (mainWindow != null)
            {
                dlgHostControl = mainWindow?.FindLogicalDescendantOfType<DialogHostControl>(true);
            }
        }
        
        // Method 4: Find over top level control
        if(dlgHostControl == null)
        {
            if (FindTopLevelLogicalParent(host) is Control topLevel)
            {
                dlgHostControl = topLevel?.FindLogicalDescendantOfType<DialogHostControl>(true);
            }
        }

        return dlgHostControl;
    }

    private static ILogical FindTopLevelLogicalParent(ILogical currentControl)
    {
        if (currentControl.LogicalParent == null) { return currentControl; }
        
        return FindTopLevelLogicalParent(currentControl.LogicalParent);
    }
}