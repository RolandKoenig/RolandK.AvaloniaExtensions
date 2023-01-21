using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.AvaloniaExtensions.ViewServices.Base;

public class ViewServiceRequestEventArgs
{
    public Type ViewServiceType { get; }

    public object? ViewService { get; set; }

    public ViewServiceRequestEventArgs(Type viewServiceType)
    {
        this.ViewServiceType = viewServiceType;
    }
}