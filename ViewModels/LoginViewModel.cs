using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Threading;
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
        Constants.SelectedPersonId = SelectedProfile?.Id;
        _mainViewModel.NavigateToMenuPage();
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
        Profiles = new ObservableCollection<Person>(data);
    }
}