namespace RolandK.AvaloniaExtensions.Mvvm;

public class ViewModelDetachedEventArgs(IAttachableViewModel viewModel) : EventArgs
{
    public IAttachableViewModel ViewModel { get; } = viewModel;
}