using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices;

public interface ISaveFileViewService : IViewService
{
    Task<string?> ShowSaveFileDialogAsync(IEnumerable<FileDialogFilter> filters, string defaultExtension);
}