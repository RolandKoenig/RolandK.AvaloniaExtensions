using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RolandK.AvaloniaExtensions.FluentThemeDetection;
using RolandK.AvaloniaExtensions.TestApp.Data;
using RolandK.AvaloniaExtensions.TestApp.Services;
using RolandK.AvaloniaExtensions.ViewServices;

namespace RolandK.AvaloniaExtensions.TestApp;

public partial class MainWindowViewModel : OwnViewModelBase
{
    private readonly ITestDataGenerator _testDataGenerator;

    [ObservableProperty] 
    private string _title = string.Empty;

    [ObservableProperty]
    private ObservableCollection<UserData> _dataRows = new();

    public MainWindowViewModel(ITestDataGenerator testDataGenerator)
    {
        _testDataGenerator = testDataGenerator;

        this.Title = "RolandK.AvaloniaExtension Test Application";
        this.DataRows = new ObservableCollection<UserData>(
            _testDataGenerator.GenerateUserData(50));
    }

    [RelayCommand]
    public void RecreateTestData()
    {
        this.DataRows = new ObservableCollection<UserData>(
            _testDataGenerator.GenerateUserData(50));
    }

    [RelayCommand]
    public async Task OpenFileAsync()
    {
        var srvOpenFile = base.GetViewService<IOpenFileViewService>();
        var srvMessageBox = base.GetViewService<IMessageBoxService>();
        
        var selectedFile =  await srvOpenFile.ShowOpenFileDialogAsync(
            Array.Empty<FileDialogFilter>(),
            "Open file");
        if (string.IsNullOrEmpty(selectedFile)) { return; }
        
        await srvMessageBox.ShowAsync(
            "Open file",
            $"File {selectedFile} selected", MessageBoxButtons.Ok);
    }

    [RelayCommand]
    public async Task ShowDummyMessageBoxAsync()
    {
        var srvMessageBox = base.GetViewService<IMessageBoxService>();

        await srvMessageBox.ShowAsync(
            "Show dummy MessageBox",
            "Dummy message",
            MessageBoxButtons.Ok);
    }

    [RelayCommand]
    public void SetTheme(string themeModeName)
    {
        Application.Current.TrySetFluentThemeMode(
            Enum.Parse<FluentThemeMode>(themeModeName));
    }

    public static MainWindowViewModel DesignViewModel => new(
        NSubstitute.Substitute.For<ITestDataGenerator>());
}