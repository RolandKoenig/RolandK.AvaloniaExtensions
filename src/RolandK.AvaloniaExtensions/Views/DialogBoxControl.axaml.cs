using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RolandK.AvaloniaExtensions.Views;

public class DialogBoxControl : UserControl
{
    private TextBlock _txtHeader;
    private Grid _gridContent;

    public string Header
    {
        get => _txtHeader.Text;
        set => _txtHeader.Text = value;
    }

    public Controls ContentArea
    {
        get => _gridContent.Children;
    }
    
    public DialogBoxControl()
    {
        AvaloniaXamlLoader.Load(this);

        _txtHeader = this.FindControl<TextBlock>("TxtHeader");
        _gridContent = this.FindControl<Grid>("GridContent");
    }
}