using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices.FileDialogs;

public class OpenFileDialogService : ViewServiceBase, IOpenFileViewService
{
    private Window _parent;

    public OpenFileDialogService(Window parent)
    {
        _parent = parent;
    }

    /// <inheritdoc />
    public async Task<string?> ShowOpenFileDialogAsync(IEnumerable<FileDialogFilter> filters, string defaultExtension)
    {
        var openOptions = new FilePickerOpenOptions();
        openOptions.FileTypeFilter = filters
            .Select(x =>
            {
                var result = new FilePickerFileType(x.Name);
                result.Patterns = x.Extensions
                    .Select(x => $"*{x}")
                    .ToList();
                return result;
            })
            .ToList();

        openOptions.AllowMultiple = false;

        var selectedFiles = await _parent.StorageProvider.OpenFilePickerAsync(openOptions);
        if ((selectedFiles == null) ||
            (selectedFiles.Count == 0))
        {
            return null;
        }
        else
        {
            return HttpUtility.UrlDecode(selectedFiles[0].Path.AbsolutePath);
        }
    }

    /// <inheritdoc />
    public async Task<string[]?> ShowOpenMultipleFilesDialogAsync(IEnumerable<FileDialogFilter> filters, string title)
    {
        var openOptions = new FilePickerOpenOptions();
        openOptions.FileTypeFilter = filters
            .Select(x =>
            {
                var result = new FilePickerFileType(x.Name);
                result.Patterns = x.Extensions
                    .Select(x => $"*{x}")
                    .ToList();
                return result;
            })
            .ToList();

        openOptions.AllowMultiple = true;

        var selectedFiles = await _parent.StorageProvider.OpenFilePickerAsync(openOptions);
        if ((selectedFiles == null) ||
            (selectedFiles.Count == 0))
        {
            return null;
        }
        else
        {
            return selectedFiles
                .Select(x => HttpUtility.UrlDecode(x.Path.AbsolutePath))
                .ToArray();
        }
    }
}