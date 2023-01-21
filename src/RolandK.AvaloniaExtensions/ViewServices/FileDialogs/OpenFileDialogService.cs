using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
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
        var dlgOpenFile = new OpenFileDialog();
        dlgOpenFile.Filters ??= new List<Avalonia.Controls.FileDialogFilter>(filters.Count());
        
        foreach (var actFilter in filters)
        {
            var actAvaloniaFilter = new global::Avalonia.Controls.FileDialogFilter();
            actAvaloniaFilter.Name = actFilter.Name;
            actAvaloniaFilter.Extensions = actFilter.Extensions;
            dlgOpenFile.Filters.Add(actAvaloniaFilter);
        }
        dlgOpenFile.AllowMultiple = false;

        var selectedFiles = await dlgOpenFile.ShowAsync(_parent);
        if ((selectedFiles == null) ||
            (selectedFiles.Length == 0))
        {
            return null;
        }
        else
        {
            return selectedFiles[0];
        }
    }

    /// <inheritdoc />
    public async Task<string[]?> ShowOpenMultipleFilesDialogAsync(IEnumerable<FileDialogFilter> filters, string title)
    {
        var dlgOpenFile = new OpenFileDialog();
        dlgOpenFile.Filters ??= new List<Avalonia.Controls.FileDialogFilter>(filters.Count());
        
        foreach (var actFilter in filters)
        {
            var actAvaloniaFilter = new global::Avalonia.Controls.FileDialogFilter();
            actAvaloniaFilter.Name = actFilter.Name;
            actAvaloniaFilter.Extensions = actFilter.Extensions;
            dlgOpenFile.Filters.Add(actAvaloniaFilter);
        }
        dlgOpenFile.AllowMultiple = true;

        var selectedFiles = await dlgOpenFile.ShowAsync(_parent);
        if ((selectedFiles == null) ||
            (selectedFiles.Length == 0))
        {
            return null;
        }
        else
        {
            return selectedFiles;
        }
    }
}