using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public event EventHandler<ViewModelAttachedEventArgs>? ViewModelAttached;

    public event EventHandler<ViewModelPropertyChangedEventArgs>? ViewModelPropertyChanged;

    public event EventHandler<ViewModelDetachedEventArgs>? ViewModelDetached; 

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

    private bool IsViewFor(Type viewModelType)
    {
        if (viewModelType == this.ViewFor)
        {
            return true;
        }

        var conventionFunc = AvaloniaExtensionsConventions.IsViewForViewModelFunc;
        if (conventionFunc?.Invoke(this.GetType(), viewModelType) == true)
        {
            return true;
        }

        return false;
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
    
    protected virtual void OnViewModelPropertyChanged(ViewModelPropertyChangedEventArgs args)
    {
        this.ViewModelPropertyChanged?.Invoke(this, args);
    }

    protected virtual void OnViewModelAttached(ViewModelAttachedEventArgs args)
    {
        this.ViewModelAttached?.Invoke(this, args);
    }

    protected virtual void OnViewModelDetached(ViewModelDetachedEventArgs args)
    {
        this.ViewModelDetached?.Invoke(this, args);
    }

    private void OnViewForChanged()
    {
        if (!_isAttachedToLogicalTree) { return; }
        
        this.DetachFromDataContext();
        this.TryAttachToDataContext();
    }

    private void TryAttachToDataContext()
    {
        if (Design.IsDesignMode) { return; }
        if (_currentlyAttachedViewModel == this.DataContext) { return; }
        
        this.DetachFromDataContext();
        if (this.DataContext is IAttachableViewModel dataContextAttachable &&
            this.IsViewFor(this.DataContext.GetType()))
        {
            if(dataContextAttachable.AssociatedView != null)
            {
                throw new InvalidOperationException(
                    $"Unable to attach to DataContext from view {this.GetType().FullName}: " +
                    $"The given DataContext of type {dataContextAttachable.GetType().FullName} " +
                    $"is already attached to a view of type {dataContextAttachable.AssociatedView.GetType().FullName}");
            }
            
            var dataContextPropertyChanged = dataContextAttachable as INotifyPropertyChanged;
            
            dataContextAttachable.ViewServiceRequest += this.OnDataContextAttachable_ViewServiceRequest;
            dataContextAttachable.CloseWindowRequest += this.OnDataContextAttachable_CloseWindowRequest;
            if (dataContextPropertyChanged != null)
            {
                dataContextPropertyChanged.PropertyChanged += this.OnDataContextAttachable_PropertyChanged;
            }
            try
            {
                dataContextAttachable.AssociatedView = this;
            }
            catch
            {
                dataContextAttachable.ViewServiceRequest -= this.OnDataContextAttachable_ViewServiceRequest;
                dataContextAttachable.CloseWindowRequest -= this.OnDataContextAttachable_CloseWindowRequest;
                if (dataContextPropertyChanged != null)
                {
                    dataContextPropertyChanged.PropertyChanged -= this.OnDataContextAttachable_PropertyChanged;
                }
                throw;
            }
            
            _currentlyAttachedViewModel = dataContextAttachable;
            this.OnViewModelAttached(new ViewModelAttachedEventArgs(dataContextAttachable));
        }
    }

    private void DetachFromDataContext()
    {
        if (_currentlyAttachedViewModel != null)
        {
            _currentlyAttachedViewModel.AssociatedView = null;
            _currentlyAttachedViewModel.ViewServiceRequest -= this.OnDataContextAttachable_ViewServiceRequest;
            _currentlyAttachedViewModel.CloseWindowRequest -= this.OnDataContextAttachable_CloseWindowRequest;
            
            if (_currentlyAttachedViewModel is INotifyPropertyChanged dataContextPropertyChanged)
            {
                dataContextPropertyChanged.PropertyChanged -= this.OnDataContextAttachable_PropertyChanged;
            }

            this.OnViewModelDetached(new ViewModelDetachedEventArgs(_currentlyAttachedViewModel));
        }
        _currentlyAttachedViewModel = null;
    }

    /// <inheritdoc />
    public object? TryGetDefaultViewService(Type viewServiceType)
    {
        return DefaultViewServices.TryGetDefaultViewService(this, viewServiceType);
    }
    
    private void OnDataContextAttachable_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (_currentlyAttachedViewModel == null) { return; }
        
        this.OnViewModelPropertyChanged(new ViewModelPropertyChangedEventArgs(
            _currentlyAttachedViewModel,
            e.PropertyName));
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