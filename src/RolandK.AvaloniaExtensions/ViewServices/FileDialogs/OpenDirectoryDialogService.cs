using System.Web;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices.FileDialogs;

public class OpenDirectoryDialogService : ViewServiceBase, IOpenDirectoryViewService
{
    private Window _parent;
    
    public OpenDirectoryDialogService(Window parent)
    {
        _parent = parent;
    }

    /// <inheritdoc />
    public async Task<string?> ShowOpenDirectoryDialogAsync(string title)
    {
        var options = new FolderPickerOpenOptions();
        options.AllowMultiple = false;
        options.Title = title;

        var selectedFolders = await _parent.StorageProvider.OpenFolderPickerAsync(options);
        if ((selectedFolders == null) ||
            (selectedFolders.Count == 0))
        {
            return null;
        }

        return HttpUtility.UrlDecode(selectedFolders[0].Path.AbsolutePath);
    }
}