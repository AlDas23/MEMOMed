using CommunityToolkit.Mvvm.ComponentModel;

namespace MEMOMed.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentViewModel;

    public MainWindowViewModel()
    {
        CurrentViewModel = new LoginViewModel(this);
    }
    
    public void NavigateToLoginPage()
    {
        CurrentViewModel = new LoginViewModel(this);
    }
    
    public void NavigateToProfileCreatePage()
    {
        CurrentViewModel = new ProfileCreateViewModel(this);
    }
    public void NavigateToMenuPage()
    {
        CurrentViewModel = new MenuViewModel(this);
    }
    public void NavigateToMeasurementsPage()
    {
        CurrentViewModel = new AddMeasurementsViewModel(this);
    }
    public void NavigateToMedicinePage()
    {
        CurrentViewModel = new MedicineConfigurationViewModel(this);
    }
}