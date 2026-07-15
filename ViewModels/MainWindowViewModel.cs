using CommunityToolkit.Mvvm.ComponentModel;
using MEMOMed.Models.DataClasses;
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

    private void SetWindowSize(double width, double height)
    {
        WindowHeight = height;
        WindowWidth = width;
    }

    public void NavigateToLoginPage()
    {
        CurrentViewModel = new LoginViewModel(this);
        SetWindowSize(400, 520);
    }

    public void NavigateToProfileCreatePage()
    {
        CurrentViewModel = new ProfileCreateViewModel(this);
        SetWindowSize(400, 520);
    }

    public void NavigateToMenuPage()
    {
        CurrentViewModel = new MenuViewModel(this);
        SetWindowSize(500, 600);
    }

    public void NavigateToMeasurementsPage()
    {
        CurrentViewModel = new AddMeasurementsViewModel(this);
        SetWindowSize(600, 500);
    }

    public void NavigateToMedicinePage()
    {
        CurrentViewModel = new MedicineConfigurationViewModel(this);
        SetWindowSize(550, 600);
    }

    public void NavigateToTablePage()
    {
        CurrentViewModel = new TableViewModel(this);
        SetWindowSize(1100, 800);
    }

    public void NavigateToEditMeasurementsPage(BodyMeasurement measurement)
    {
        CurrentViewModel = new EditMeasurementsViewModel(this, measurement);
        SetWindowSize(600, 500);
    }

    public void NavigateToEditMeasurementsPage(HeartMeasurement measurement)
    {
        CurrentViewModel = new EditMeasurementsViewModel(this, measurement);
        SetWindowSize(600, 500);
    }
    
}