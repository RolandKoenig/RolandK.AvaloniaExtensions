using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm.Controls;

public class MvvmUserControl : UserControl, IViewServiceHost
{
    private IAttachableViewModel? _currentlyAttachedViewModel;
    private bool _isAttachedToLogicalTree;
    private ViewServiceContainer _viewServiceContainer;
    private Type? _viewFor;

    public Type? ViewFor
    {
        get => _viewFor;
        set
        {
            if (_viewFor == value) { return; }

            _viewFor = value;
            this.OnViewForChanged();
        }
    }

    /// <inheritdoc />
    public ICollection<IViewService> ViewServices => _viewServiceContainer.ViewServices;

    /// <inheritdoc />
    public IViewServiceHost? ParentViewServiceHost => this.TryGetParentViewServiceHost();

    public MvvmUserControl()
    {
        _viewServiceContainer = new ViewServiceContainer(this);
    }

    public MvvmUserControl(Control initialChild)
        : this()
    {
        this.Content = initialChild;
    }

    /// <inheritdoc />
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        _isAttachedToLogicalTree = true;
        this.TryAttachToDataContext();
        
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
            return;
        }
        
        this.DetachFromDataContext();

        base.OnDataContextChanged(e);

        this.TryAttachToDataContext();
    }

    private void OnViewForChanged()
    {
        if (!_isAttachedToLogicalTree) { return; }
        
        this.DetachFromDataContext();
        this.TryAttachToDataContext();
    }

    private void TryAttachToDataContext()
    {
        if (_currentlyAttachedViewModel == this.DataContext) { return; }
        
        this.DetachFromDataContext();
        if ((this.DataContext is IAttachableViewModel dataContextAttachable) &&
            (this.DataContext.GetType() == this.ViewFor))
        {
            if(dataContextAttachable.AssociatedView != null)
            {
                throw new InvalidOperationException(
                    $"Unable to attach to DataContext from view {this.GetType().FullName}: " +
                    $"The given DataContext of type {dataContextAttachable.GetType().FullName} " +
                    $"is already attached to a view of type {dataContextAttachable.AssociatedView.GetType().FullName}");
            }
            
            dataContextAttachable.ViewServiceRequest += this.OnDataContextAttachable_ViewServiceRequest;
            dataContextAttachable.CloseWindowRequest += this.OnDataContextAttachable_CloseWindowRequest;
            try
            {
                dataContextAttachable.AssociatedView = this;
            }
            catch
            {
                dataContextAttachable.ViewServiceRequest -= this.OnDataContextAttachable_ViewServiceRequest;
                dataContextAttachable.CloseWindowRequest -= this.OnDataContextAttachable_CloseWindowRequest;
                throw;
            }
            _currentlyAttachedViewModel = dataContextAttachable;
        }
    }

    private void DetachFromDataContext()
    {
        if (_currentlyAttachedViewModel != null)
        {
            _currentlyAttachedViewModel.ViewServiceRequest -= this.OnDataContextAttachable_ViewServiceRequest;
            _currentlyAttachedViewModel.CloseWindowRequest -= this.OnDataContextAttachable_CloseWindowRequest;
            _currentlyAttachedViewModel.AssociatedView = null;
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
    
    private void OnDataContextAttachable_CloseWindowRequest(object? sender, CloseWindowRequestEventArgs e)
    {
        var parentWindow = this.FindLogicalAncestorOfType<Window>();
        if (parentWindow == null) { return; }
        
        if (e.DialogResult != null)
        {
            parentWindow.Close(e.DialogResult);
        }
        else
        {
            parentWindow.Close();
        }
    }
}