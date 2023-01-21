using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices;

public interface IOpenDirectoryViewService : IViewService
{
    Task<string?> ShowOpenDirectoryDialogAsync(string title);
}