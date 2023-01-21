using System;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm;

public interface IAttachableViewModel
{
    event EventHandler<CloseWindowRequestEventArgs>? CloseWindowRequest;

    event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

    object? AssociatedView
    {
        get;
        set;
    }
}