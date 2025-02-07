using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Platform;
using RolandK.AvaloniaExtensions.ViewServices;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.Mvvm.Controls;

public class MvvmWindow : Window, IViewServiceHost
{
    private IAttachableViewModel? _currentlyAttachedViewModel;
    private bool _isOpened;
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
    
    public MvvmWindow()
    {
        _viewServiceContainer = new ViewServiceContainer(this);
    }

    public MvvmWindow(IWindowImpl windowImpl)
        : base(windowImpl)
    {
        _viewServiceContainer = new ViewServiceContainer(this);
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
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        
        _isOpened = true;
        this.TryAttachToDataContext();
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

            this.TryAttachToDataContext();
        }
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
        if (!_isOpened) { return; }
        
        this.DetachFromDataContext();
        this.TryAttachToDataContext();
    }
    
    private void TryAttachToDataContext()
    {
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
            _currentlyAttachedViewModel.CloseWindowRequest -= this.OnDataContextAttachable_CloseWindowRequest;
            _currentlyAttachedViewModel.ViewServiceRequest -= this.OnDataContextAttachable_ViewServiceRequest;
            
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