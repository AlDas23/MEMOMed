using CommunityToolkit.Mvvm.Input;

namespace MEMOMed.ViewModels;

public partial class MenuViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    public MenuViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
    }
    
    // ONLY for designer
    // public MenuViewModel()
    // {
    //     _mainWindowViewModel = null;
    // }

    [RelayCommand]
    private void SwitchToAddMeasurements()
    {
        _mainWindowViewModel.NavigateToMeasurementsPage();
    }
    [RelayCommand]
    private void SwitchToMedicineConfiguration()
    {
        _mainWindowViewModel.NavigateToMedicinePage();
    }
    [RelayCommand]
    private void SwitchToViewMeasurements()
    {
        _mainWindowViewModel.NavigateToTablePage();
    }
    [RelayCommand]
    private void SwitchToGraphPage()
    {
        // switch to graph page
    }

    [RelayCommand]
    private void ReturnBackToLogin()
    {
        _mainWindowViewModel.NavigateToLoginPage();
    }
}