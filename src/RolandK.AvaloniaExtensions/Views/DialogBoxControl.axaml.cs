using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.Views;

public partial class DialogBoxControl : UserControl
{
    private TextBlock _txtHeader;
    private Grid _gridContent;

    public string Header
    {
        get => _txtHeader.Text ?? string.Empty;
        set => _txtHeader.Text = value;
    }

    public Controls ContentArea
    {
        get => _gridContent.Children;
    }
    
    public DialogBoxControl()
    {
        AvaloniaXamlLoader.Load(this);

        _txtHeader = this.FindControl<TextBlock>("TxtHeader") ?? throw new InvalidOperationException("Control TxtHeader not found!");
        _gridContent = this.FindControl<Grid>("GridContent") ?? throw new InvalidOperationException("Control GridContent not found!");
    }
}