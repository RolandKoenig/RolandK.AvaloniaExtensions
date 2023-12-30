using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices;

public interface IOpenDirectoryViewService : IViewService
{
    Task<string?> ShowOpenDirectoryDialogAsync(string title);
}