using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Platform;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm.Markup;

public class MvvmWindow : Window, IViewServiceHost
{
    private IAttachableViewModel? _currentlyAttachedViewModel;
    private bool _isOpened;
    private ViewServiceContainer _viewServiceContainer;

    /// <inheritdoc />
    public ICollection<IViewService> ViewServices => _viewServiceContainer.ViewServices;

    /// <inheritdoc />
    public IViewServiceHost? ParentViewServiceHost => this.TryGetParentViewServiceHost();
    
    public MvvmWindow()
    {
        _viewServiceContainer = new ViewServiceContainer(this);
    }

    public MvvmWindow(IWindowImpl windowImpl)
        : base(windowImpl)
    {
        _viewServiceContainer = new ViewServiceContainer(this);
    }

    /// <inheritdoc />
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        
        _isOpened = true;
        this.AttachToDataContext();
    }

    /// <inheritdoc />
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        
        _isOpened = false;
        this.DetachFromDataContext();
    }

    /// <inheritdoc />
    protected override void OnDataContextChanged(EventArgs e)
    {
        if (!_isOpened)
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
            _currentlyAttachedViewModel.AssociatedView = null;
            _currentlyAttachedViewModel.CloseWindowRequest -= this.OnDataContextAttachable_CloseWindowRequest;
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
    
    private void OnDataContextAttachable_CloseWindowRequest(object sender, CloseWindowRequestEventArgs e)
    {
        if (e.DialogResult != null)
        {
            this.Close(e.DialogResult);
        }
        else
        {
            this.Close();
        }
    }
}