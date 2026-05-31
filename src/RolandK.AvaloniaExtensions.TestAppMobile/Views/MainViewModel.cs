using CommunityToolkit.Mvvm.ComponentModel;

namespace RolandK.AvaloniaExtensions.TestAppMobile.Views;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _greeting = "Welcome to Avalonia!";
}