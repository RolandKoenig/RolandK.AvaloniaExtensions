using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
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
    public Task<string?> ShowSaveFileDialogAsync(IEnumerable<FileDialogFilter> filters, string defaultExtension)
    {
        var dlgSaveFile = new SaveFileDialog();
        dlgSaveFile.Filters ??= new List<Avalonia.Controls.FileDialogFilter>(filters.Count());
        
        foreach (var actFilter in filters)
        {
            var actAvaloniaFilter = new global::Avalonia.Controls.FileDialogFilter();
            actAvaloniaFilter.Name = actFilter.Name;
            actAvaloniaFilter.Extensions = actFilter.Extensions;
            dlgSaveFile.Filters.Add(actAvaloniaFilter);
        }
        dlgSaveFile.DefaultExtension = defaultExtension;

        return dlgSaveFile.ShowAsync(_parent);
    }
}