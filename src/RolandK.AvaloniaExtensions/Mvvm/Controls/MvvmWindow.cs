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