namespace RolandK.AvaloniaExtensions.Mvvm;

public class ViewModelAttachedEventArgs(IAttachableViewModel viewModel) : EventArgs
{
    public IAttachableViewModel ViewModel { get; } = viewModel;
}