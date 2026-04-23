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
        // switch to measurement page
    }
    [RelayCommand]
    private void SwitchToViewMeasurements()
    {
        // switch to view measurement page
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