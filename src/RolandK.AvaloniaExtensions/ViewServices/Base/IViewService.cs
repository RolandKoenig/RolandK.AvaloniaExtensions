using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.AvaloniaExtensions.ViewServices.Base;

public interface IViewService
{
    event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;
}