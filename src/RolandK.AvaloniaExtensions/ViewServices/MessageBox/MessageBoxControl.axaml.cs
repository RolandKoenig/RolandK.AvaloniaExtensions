using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using RolandK.AvaloniaExtensions.Controls;

// Simple message box implementation
// Based on: https://stackoverflow.com/questions/55706291/how-to-show-a-message-box-in-avaloniaui-beta

namespace RolandK.AvaloniaExtensions.ViewServices.MessageBox;

public partial class MessageBoxControl : UserControl
{
    public MessageBoxControl()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static Task<MessageBoxResult> ShowAsync(DialogHostControl host, string title, string text, MessageBoxButtons buttons)
    {
        var tcs = new TaskCompletionSource<MessageBoxResult>();

        var msgbox = new MessageBoxControl();
        msgbox.FindControl<TextBlock>("Text")!.Text = text;
        var buttonPanel = msgbox.FindControl<StackPanel>("Buttons")!;

        var res = MessageBoxResult.Ok;

        void AddButton(string caption, MessageBoxResult r, bool def = false)
        {
            var btn = new Button {Content = caption};
            btn.Click += (_, __) => { 
                res = r;
                host.CloseDialog();
                tcs.TrySetResult(res);
            };
            buttonPanel.Children.Add(btn);
            if (def)
            {
                res = r;
            }
        }

        switch (buttons)
        {
            case MessageBoxButtons.Ok:
            case MessageBoxButtons.OkCancel:
                AddButton("Ok", MessageBoxResult.Ok, true);
                break;

            case MessageBoxButtons.YesNo:
            case MessageBoxButtons.YesNoCancel:
                AddButton("Yes", MessageBoxResult.Yes, true);
                AddButton("No", MessageBoxResult.No);
                break;
        }

        if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel)
        {
            AddButton("Cancel", MessageBoxResult.Cancel, true);
        }

        host.ShowDialog(msgbox, title);

        return tcs.Task;
    }
}