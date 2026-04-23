using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;

namespace MEMOMed.ViewModels;

public partial class ProfileCreateViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainViewModel;
    [ObservableProperty] private string _firstName =  string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _errorMessage =  string.Empty;
    [ObservableProperty] private bool _showErrorPopup;

    [RelayCommand]
    private void CreateProfile()
    {
        const string regex = @"([a-zA-Z]+)";
        ErrorMessage = string.Empty;
        ShowErrorPopup = false;
        var personDao = new PersonDao();

        if (Regex.IsMatch(FirstName, regex) && FirstName != string.Empty)
        {
            if (Regex.IsMatch(LastName, regex) && LastName != string.Empty)
            {
                Person person = new Person()
                {
                    FirstName = FirstName,
                    LastName = LastName
                };
                personDao.InsertEntity(person);
                _mainViewModel.NavigateToLoginPage();
            }
            else
            {
                ErrorMessage = "Illegal first or last name";
                ShowErrorPopup = true;
            }
        }
        else
        {
            ErrorMessage = "Illegal first or last name";
            ShowErrorPopup = true;
        }
    }

    [RelayCommand]
    private void CloseError() => ShowErrorPopup = false;

    public ProfileCreateViewModel(MainWindowViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }
}