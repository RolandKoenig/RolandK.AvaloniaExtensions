using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm;

public class MvvmUserControl : UserControl, IViewServiceHost
{
    private IAttachableViewModel? _currentlyAttachedViewModel;
    private bool _isAttachedToLogicalTree;
    private ViewServiceContainer _viewServiceContainer;

    /// <inheritdoc />
    public ICollection<IViewService> ViewServices => _viewServiceContainer.ViewServices;

    /// <inheritdoc />
    public IViewServiceHost? ParentViewServiceHost => this.TryGetParentViewServiceHost();

    public MvvmUserControl()
    {
        _viewServiceContainer = new ViewServiceContainer(this);
    }
    
    /// <inheritdoc />
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        _isAttachedToLogicalTree = true;
        this.AttachToDataContext();
        
        base.OnAttachedToLogicalTree(e);
    }

    /// <inheritdoc />
    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        _isAttachedToLogicalTree = false;
        this.DetachFromDataContext();
        
        base.OnDetachedFromLogicalTree(e);
    }

    /// <inheritdoc />
    protected override void OnDataContextChanged(EventArgs e)
    {
        if (!_isAttachedToLogicalTree)
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
            dataContextAttachable.ViewServiceRequest += this.OnDataContextAttachable_ViewServiceRequest;
            _currentlyAttachedViewModel = dataContextAttachable;
        }
    }

    private void DetachFromDataContext()
    {
        if (_currentlyAttachedViewModel != null)
        {
            _currentlyAttachedViewModel.AssociatedView = null;
            _currentlyAttachedViewModel.ViewServiceRequest -= this.OnDataContextAttachable_ViewServiceRequest;
        }
        _currentlyAttachedViewModel = null;
    }

    /// <inheritdoc />
    public object? TryGetDefaultViewService(Type viewServiceType)
    {
        return DefaultViewServices.TryGetDefaultViewService(this, viewServiceType);
    }
    
    private void OnDataContextAttachable_ViewServiceRequest(object? sender, ViewServiceRequestEventArgs e)
    {
        var viewService = this.TryFindViewService(e.ViewServiceType);
        if (viewService != null)
        {
            e.ViewService = viewService;
        }
    }
}