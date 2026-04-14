using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;

namespace MEMOMed.ViewModels;

public partial class ProfileCreateViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _firstName;
    [ObservableProperty]
    private string _lastName;

    [RelayCommand]
    private void CreateProfile()
    {
        const string regex = @"([a-zA-Z]+)";
        var personDao = new PersonDao();

        if (Regex.IsMatch(FirstName, regex))
        {
            if (Regex.IsMatch(LastName, regex))
            {
                Person person = new Person()
                {
                    FirstName = FirstName,
                    LastName = LastName
                };
                personDao.InsertEntity(person);
            }
            else
            {
                // Give warning
            }
        }
        else
        {
            // Give warning
        }
    }
}