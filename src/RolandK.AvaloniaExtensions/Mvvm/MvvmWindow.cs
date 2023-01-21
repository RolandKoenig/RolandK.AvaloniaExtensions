using System;
using System.Collections.Generic;
using Avalonia.Controls;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm;

public class MvvmWindow : Window, IViewServiceHost
{
    private IAttachableViewModel? _currentlyAttachedViewModel;
    private bool _isActivated;
    private ViewServiceContainer _viewServiceContainer;

    /// <inheritdoc />
    public ICollection<IViewService> ViewServices => _viewServiceContainer.ViewServices;

    /// <inheritdoc />
    public IViewServiceHost? ParentViewServiceHost => this.TryGetParentViewServiceHost();
    
    public MvvmWindow()
    {
        _viewServiceContainer = new ViewServiceContainer(this);
        
        this.Activated += this.OnActivated;
        this.Deactivated += this.OnDeactivated;
    }

    private void OnActivated(object? sender, EventArgs e)
    {
        _isActivated = true;
        this.AttachToDataContext();
    }
    
    private void OnDeactivated(object? sender, EventArgs e)
    {
        _isActivated = false;
        this.DetachFromDataContext();
    }
    
    /// <inheritdoc />
    protected override void OnDataContextChanged(EventArgs e)
    {
        if (!_isActivated)
        {
            base.OnDataContextChanged(e);
        }
        else
        {
            this.DetachFromDataContext();

            base.OnDataContextChanged(e);

            this.AttachToDataContext();
        }
    }

    private void AttachToDataContext()
    {
        if (_currentlyAttachedViewModel == this.DataContext) { return; }
        
        this.DetachFromDataContext();
        if (this.DataContext is IAttachableViewModel dataContextAttachable)
        {
            dataContextAttachable.AssociatedView = this;
            _currentlyAttachedViewModel = dataContextAttachable;
        }
    }

    private void DetachFromDataContext()
    {
        if (_currentlyAttachedViewModel != null)
        {
            _currentlyAttachedViewModel.AssociatedView = null;
        }
        _currentlyAttachedViewModel = null;
    }

    /// <inheritdoc />
    public object? TryGetDefaultViewService(Type viewServiceType)
    {
        return DefaultViewServices.TryGetDefaultViewService(this, viewServiceType);
    }
}