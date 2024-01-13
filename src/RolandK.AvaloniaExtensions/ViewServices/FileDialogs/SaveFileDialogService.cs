using System.Web;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices.FileDialogs;

public class SaveFileDialogService : ViewServiceBase, ISaveFileViewService
{
    private Window _parent;

    public SaveFileDialogService(Window parent)
    {
        _parent = parent;
    }

    /// <inheritdoc />
    public async Task<string?> ShowSaveFileDialogAsync(IEnumerable<FileDialogFilter> filters, string defaultExtension)
    {
        var fileTypes = new List<FilePickerFileType>();
        foreach (var actFilter in filters)
        {
            var actAvaloniaFilter = new FilePickerFileType(actFilter.Name);
            actAvaloniaFilter.Patterns = actFilter.Extensions
                .Select(x => $"*{x}")
                .ToList();
            fileTypes.Add(actAvaloniaFilter);
        }

        var filePickerSaveOptions = new FilePickerSaveOptions();
        filePickerSaveOptions.DefaultExtension = defaultExtension;
        filePickerSaveOptions.FileTypeChoices = fileTypes;

        var file = await _parent.StorageProvider.SaveFilePickerAsync(filePickerSaveOptions);
        if (file != null)
        {
            return HttpUtility.HtmlDecode(file.Path.AbsolutePath);
        }
        
        return null;
    }
}