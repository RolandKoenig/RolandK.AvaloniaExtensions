using CommunityToolkit.Mvvm.ComponentModel;

namespace RolandK.AvaloniaExtensions.TestApp.Views;

public partial class ResponsiveTwoColumnViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _firstColumnVisible = true;
    
    [ObservableProperty]
    private bool _secondColumnVisible = true;
    
    [ObservableProperty]
    private bool _thirdColumnVisible = true;
    
    [ObservableProperty]
    private bool _fourthColumnVisible = true;
}