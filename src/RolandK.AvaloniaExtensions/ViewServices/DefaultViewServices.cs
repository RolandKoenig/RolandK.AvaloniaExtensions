using System;
using Avalonia.Controls;
using Avalonia.VisualTree;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices.FileDialogs;
using RolandK.AvaloniaExtensions.ViewServices.MessageBox;

namespace RolandK.AvaloniaExtensions.ViewServices;

internal static class DefaultViewServices
{
    public static object? TryGetDefaultViewService(IControl host, Type viewServiceType)
    {
        if (viewServiceType == typeof(IOpenFileViewService))
        {
            var parentWindow = host.FindAncestorOfType<Window>();
            if (parentWindow == null) { return null; }
            
            return new OpenFileDialogService(parentWindow);
        }
        
        if (viewServiceType == typeof(ISaveFileViewService))
        {
            var parentWindow = host.FindAncestorOfType<Window>();
            if (parentWindow == null) { return null; }

            return new SaveFileDialogService(parentWindow);
        }
        
        if (viewServiceType == typeof(IMessageBoxService))
        {
            var dlgHostControl = TryFindDialogHostControl(host);
            if (dlgHostControl == null) { return null; }
            
            return new MessageBoxControlService(dlgHostControl);
        }
        
        return null;
    }

    private static DialogHostControl? TryFindDialogHostControl(IControl host)
    {
        var dlgHostControl = host.FindAncestorOfType<DialogHostControl>();
        if (dlgHostControl == null)
        {
            var mainWindowFrame = host.FindAncestorOfType<MainWindowFrame>();
            dlgHostControl = mainWindowFrame?.Overlay;
        }
        if (dlgHostControl == null)
        {
            var mainWindow = host.FindAncestorOfType<Window>();
            dlgHostControl = mainWindow?.FindDescendantOfType<DialogHostControl>();
        }

        return dlgHostControl;
    }
}