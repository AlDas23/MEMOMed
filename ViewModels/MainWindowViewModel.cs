using CommunityToolkit.Mvvm.ComponentModel;
using MEMOMed.Models.DataClasses.DataAccessObjects;

namespace MEMOMed.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _currentViewModel;
    [ObservableProperty] private double _windowWidth = 400;
    [ObservableProperty] private double _windowHeight = 520;


    public MainWindowViewModel()
    {
        if (PersonDao.IsEmpty())
        {
            CurrentViewModel = new ProfileCreateViewModel(this);
        }
        else
        {
            CurrentViewModel = new LoginViewModel(this);
        }
    }

    public void NavigateToLoginPage()
    {
        CurrentViewModel = new LoginViewModel(this);
        WindowWidth = 400;
        WindowHeight = 520;
    }

    public void NavigateToProfileCreatePage()
    {
        CurrentViewModel = new ProfileCreateViewModel(this);
        WindowWidth = 400;
        WindowHeight = 520;
    }

    public void NavigateToMenuPage()
    {
        CurrentViewModel = new MenuViewModel(this);
        WindowWidth = 500;
        WindowHeight = 600;
    }

    public void NavigateToMeasurementsPage()
    {
        CurrentViewModel = new AddMeasurementsViewModel(this);
        WindowWidth = 600;
        WindowHeight = 500;
    }

    public void NavigateToMedicinePage()
    {
        CurrentViewModel = new MedicineConfigurationViewModel(this);
        WindowWidth = 550;
        WindowHeight = 600;
    }

    public void NavigateToTablePage()
    {
        CurrentViewModel = new TableViewModel(this);
        WindowWidth = 800;
        WindowHeight = 600;
    }
}