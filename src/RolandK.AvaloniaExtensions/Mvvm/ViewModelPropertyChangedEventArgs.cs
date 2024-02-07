namespace RolandK.AvaloniaExtensions.Mvvm;

public class ViewModelPropertyChangedEventArgs(IAttachableViewModel viewModel, string propertyName) : EventArgs
{
    public IAttachableViewModel ViewModel { get; } = viewModel;

    public string PropertyName { get; } = propertyName;
}