using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;

namespace MEMOMed.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Person>? _profiles;
    [ObservableProperty]
    private Person? _selectedProfile;

    [RelayCommand]
    private void SelectProfile()
    {
        Constants.SelectedPerson = SelectedProfile;
        // Redirect on Profile view
    }

    public MainWindowViewModel()
    {
        var pDao = new PersonDao();
        var data = pDao.GetAllEntities();
        if (data is null || data.Count == 0)
        {
            // Redirect on profile creation view
        }
        else
        {
            Profiles = new ObservableCollection<Person>(data);
        }
    }
}