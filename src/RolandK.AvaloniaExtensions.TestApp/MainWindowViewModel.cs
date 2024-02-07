using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        var srvOpenFile = this.GetViewService<IOpenFileViewService>();
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();
        
        var selectedFile =  await srvOpenFile.ShowOpenFileDialogAsync(
            Array.Empty<FileDialogFilter>(),
            "Open file");
        if (string.IsNullOrEmpty(selectedFile)) { return; }
        
        await srvMessageBox.ShowAsync(
            "Open file",
            $"File {selectedFile} selected", 
            MessageBoxButtons.Ok);
    }

    [RelayCommand]
    public async Task OpenDirectoryAsync()
    {
        var srvOpenDirectory = this.GetViewService<IOpenDirectoryViewService>();
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();

        var selectedDirectory = await srvOpenDirectory.ShowOpenDirectoryDialogAsync(
            "Open directory");
        if (string.IsNullOrEmpty(selectedDirectory)) { return; }

        await srvMessageBox.ShowAsync(
            "Open directory",
            $"Directory {selectedDirectory} selected",
            MessageBoxButtons.Ok);
    }

    [RelayCommand]
    public async Task ShowDummyMessageBoxAsync()
    {
        var srvMessageBox = this.GetViewService<IMessageBoxViewService>();

        await srvMessageBox.ShowAsync(
            "Show dummy MessageBox",
            "Dummy message",
            MessageBoxButtons.Ok);
    }

    [RelayCommand]
    public void Exit()
    {
        this.CloseHostWindow();
    }

    public static MainWindowViewModel DesignViewModel => new(
        NSubstitute.Substitute.For<ITestDataGenerator>());
}