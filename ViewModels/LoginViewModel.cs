using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;

namespace MEMOMed.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainViewModel;
    [ObservableProperty] private ObservableCollection<Person>? _profiles;
    [ObservableProperty] private Person? _selectedProfile;

    [RelayCommand]
    private void SelectProfile()
    {
        Constants.SelectedPerson = SelectedProfile;
        // _mainViewModel.CurrentViewModel = new ViewModel for menu page
    }

    [RelayCommand]
    private void CreateProfile()
    {
        _mainViewModel.NavigateToProfileCreatePage();
    }

    public LoginViewModel(MainWindowViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        var pDao = new PersonDao();
        var data = pDao.GetAllEntities();
        if (data is null || data.Count == 0)
        {
            _mainViewModel.NavigateToProfileCreatePage();
        }
        else
        {
            Profiles = new ObservableCollection<Person>(data);
        }
    }
}