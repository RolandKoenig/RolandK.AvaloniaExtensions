using System.Threading.Tasks;
using RolandK.AvaloniaExtensions.Views;
using RolandK.AvaloniaExtensions.ViewServices.Base;

namespace RolandK.AvaloniaExtensions.ViewServices.MessageBox;

public class MessageBoxViewControlService : ViewServiceBase, IMessageBoxViewService
{
    private DialogHostControl _host;

    public MessageBoxViewControlService(DialogHostControl host)
    {
        _host = host;
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowAsync(string title, string message, MessageBoxButtons buttons)
    {
        return MessageBoxControl.ShowAsync(_host, title, message, buttons);
    }
}