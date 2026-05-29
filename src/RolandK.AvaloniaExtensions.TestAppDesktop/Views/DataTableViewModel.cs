using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RolandK.AvaloniaExtensions.TestAppDesktop.Data;
using RolandK.AvaloniaExtensions.TestAppDesktop.Services;

namespace RolandK.AvaloniaExtensions.TestAppDesktop.Views;

public partial class DataTableViewModel : ObservableObject
{
    private readonly ITestDataGenerator _testDataGenerator;
        
    [ObservableProperty]
    private ObservableCollection<UserData> _dataRows = new();
    
    public DataTableViewModel(ITestDataGenerator testDataGenerator)
    {
        _testDataGenerator = testDataGenerator;
        
        this.DataRows = new ObservableCollection<UserData>(
            _testDataGenerator.GenerateUserData(50));
    }
    
    [RelayCommand]
    private void RecreateTestData()
    {
        this.DataRows = new ObservableCollection<UserData>(
            _testDataGenerator.GenerateUserData(50));
    }
    
    public static DataTableViewModel DesignViewModel => new(
        NSubstitute.Substitute.For<ITestDataGenerator>());
}