using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace RolandK.AvaloniaExtensions.ViewServices.Base;

public class ViewServiceContainer
{
    public ObservableCollection<IViewService> ViewServices { get; } = new ObservableCollection<IViewService>();
    public IViewServiceHost Owner { get; }

    public ViewServiceContainer(IViewServiceHost owner)
    {
        this.Owner = owner;
        this.ViewServices.CollectionChanged += this.OnViewServices_CollectionChanged;
    }

    private void OnViewServices_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (IViewService? actNewItem in e.NewItems)
            {
                if(actNewItem == null){ continue; }

                actNewItem.ViewServiceRequest += this.OnViewServiceRequest;
            }
        }
        if (e.OldItems != null)
        {
            foreach (IViewService? actOldItem in e.OldItems)
            {
                if(actOldItem == null){ continue; }

                actOldItem.ViewServiceRequest -= this.OnViewServiceRequest;
            }
        }
    }

    private void OnViewServiceRequest(object? sender, ViewServiceRequestEventArgs e)
    {
        var foundViewService = this.Owner.TryFindViewService(e.ViewServiceType);
        if (foundViewService != null)
        {
            e.ViewService = foundViewService;
        }
    }
}