using System;
using System.Collections.Generic;
using System.Text;

namespace RolandK.AvaloniaExtensions.ViewServices.Base;

public class ViewServiceBase : IViewService
{
    public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

    /// <summary>
    /// Gets the view service of the given type.
    /// </summary>
    protected T GetViewService<T>()
        where T : class
    {
        var eventArgs = new ViewServiceRequestEventArgs(typeof(T));
        this.ViewServiceRequest?.Invoke(this, eventArgs);

        if (!(eventArgs.ViewService is T result))
        {
            throw new ApplicationException($"Unable to get view service of type {typeof(T).FullName}!");
        }
        return result;
    }
}