using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices;

public interface IMessageBoxViewService : IViewService
{
    Task<MessageBoxResult> ShowAsync(string title, string message, MessageBoxButtons buttons);
}