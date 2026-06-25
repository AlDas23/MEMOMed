using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;
using MEMOMed.Models.DataClasses.Enums;
using MEMOMed.ViewModels.DataWrapperVM;

namespace MEMOMed.ViewModels;

public partial class MedicineConfigurationViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private MedicineDao _medicineDao;
    private EPageType _pageType;
    [ObservableProperty] private bool _isCreateMedicinePage;
    [ObservableProperty] private bool _isAssignMedicinePage;
    [ObservableProperty] private string? _nameField;
    [ObservableProperty] private string? _descriptionField;
    [ObservableProperty] private bool _isMorningSelected;
    [ObservableProperty] private bool _isAfternoonSelected;
    [ObservableProperty] private bool _isEveningSelected;
    [ObservableProperty] private bool _isNightSelected;
    [ObservableProperty] private bool _isMondaySelected;
    [ObservableProperty] private bool _isTuesdaySelected;
    [ObservableProperty] private bool _isWednesdaySelected;
    [ObservableProperty] private bool _isThursdaySelected;
    [ObservableProperty] private bool _isFridaySelected;
    [ObservableProperty] private bool _isSaturdaySelected;
    [ObservableProperty] private bool _isSundaySelected;
    [ObservableProperty] private ObservableCollection<MedicineWrapperViewModel>? _medicine;
    [ObservableProperty] private bool _isError;
    [ObservableProperty] private string? _errorMessage;

    // For designer ONLY
    // public MedicineConfigurationViewModel()
    // {
    //     _mainWindowViewModel = null;
    //     _medicineDao = new MedicineDao();
    //     var medicineList = _medicineDao.GetAllEntities();
    //     if (medicineList != null)
    //     {
    //         Medicine = new ObservableCollection<MedicineWrapperViewModel>(
    //             medicineList.Select(m => new MedicineWrapperViewModel(m))
    //         );
    //     }
    //     else
    //     {
    //         Console.WriteLine("No Medicine Data");
    //     }
    //     ChangePage(EPageType.Medicine);
    // }

    public MedicineConfigurationViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _medicineDao = new MedicineDao();
        var medicineList = _medicineDao.GetAllEntities();
        if (medicineList.Count != 0)
        {
            var assignedMedicineIds = _medicineDao.GetAllAssignedMedicine(Constants.SelectedPersonId.Value);
            Medicine = new ObservableCollection<MedicineWrapperViewModel>(
                medicineList.Select(m =>
                    assignedMedicineIds.Contains((int)m.Id)
                        ? new MedicineWrapperViewModel(m, true)
                        : new MedicineWrapperViewModel(m))
            );
        }
        else
        {
            Console.WriteLine("No Medicine Data");
            _mainWindowViewModel.NavigateToMenuPage();
        }

        ChangePage(EPageType.Medicine);
    }

    private void ChangePage(EPageType pageType)
    {
        _pageType = pageType;
        if (_pageType == EPageType.Medicine)
        {
            IsCreateMedicinePage = true;
            IsAssignMedicinePage = false;
        }
        else
        {
            IsCreateMedicinePage = false;
            IsAssignMedicinePage = true;
        }
    }

    private List<EDayTime> TimeSchedule
    {
        get
        {
            var selected = new List<EDayTime>();
            if (IsMorningSelected) selected.Add(EDayTime.Morning);
            if (IsAfternoonSelected) selected.Add(EDayTime.Afternoon);
            if (IsEveningSelected) selected.Add(EDayTime.Evening);
            if (IsNightSelected) selected.Add(EDayTime.Night);
            return selected;
        }
    }

    private List<EWeekday> DaySchedule
    {
        get
        {
            var selected = new List<EWeekday>();
            if (IsMondaySelected) selected.Add(EWeekday.Monday);
            if (IsTuesdaySelected) selected.Add(EWeekday.Tuesday);
            if (IsWednesdaySelected) selected.Add(EWeekday.Wednesday);
            if (IsThursdaySelected) selected.Add(EWeekday.Thursday);
            if (IsFridaySelected) selected.Add(EWeekday.Friday);
            if (IsSaturdaySelected) selected.Add(EWeekday.Saturday);
            if (IsSundaySelected) selected.Add(EWeekday.Sunday);
            return selected;
        }
    }

    private void SubmitMedicine()
    {
        if (string.IsNullOrEmpty(NameField))
        {
            var em = "Invalid Medicine Data: Error in field \"NAME\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(DescriptionField))
        {
            var em = "Invalid Medicine Data: Error in field \"DESCRIPTION\"!";
            throw new Exception(em);
        }

        var timeSchedule = TimeSchedule;
        var daySchedule = DaySchedule;

        Medicine medicine = new Medicine(null, NameField, DescriptionField, timeSchedule, daySchedule);
        _medicineDao = new MedicineDao();
        _medicineDao.InsertEntity(medicine);
    }

    private void SubmitMedicineAssign()
    {
        if (Medicine is null)
        {
            Console.WriteLine("Medicine is Empty");
            return;
        }

        List<int> medicineId = [];
        foreach (var med in Medicine)
        {
            if (med.IsSelected)
            {
                var medId = med.Medicine.Id;
                medicineId.Add(medId.Value);
            }
        }

        var personDao = new PersonDao();
        personDao.UpdateEntityMedicineList(Constants.SelectedPersonId.Value, medicineId);
    }

    [RelayCommand]
    private void SubmitForm()
    {
        if (Constants.SelectedPersonId == null)
        {
            Console.WriteLine("No Person Selected. Returning to Login page...");
            _mainWindowViewModel.NavigateToLoginPage();
        }

        if (IsError)
        {
            IsError = false;
        }

        switch (_pageType)
        {
            case EPageType.Medicine:
                try
                {
                    SubmitMedicine();
                    goto default;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                    break;
                }

            case EPageType.MedicineAssign:
                try
                {
                    SubmitMedicineAssign();
                    goto default;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                    break;
                }
            default:
                NameField = string.Empty;
                DescriptionField = string.Empty;
                IsMorningSelected = false;
                IsAfternoonSelected = false;
                IsEveningSelected = false;
                IsNightSelected = false;
                IsMondaySelected = false;
                IsTuesdaySelected = false;
                IsWednesdaySelected = false;
                IsThursdaySelected = false;
                IsFridaySelected = false;
                IsSaturdaySelected = false;
                IsSundaySelected = false;
                ErrorMessage = string.Empty;
                break;
        }
    }

    [RelayCommand]
    private void MedicineChange()
    {
        ChangePage(EPageType.Medicine);
    }

    [RelayCommand]
    private void MedicineAssignChange()
    {
        ChangePage(EPageType.MedicineAssign);
    }

    [RelayCommand]
    private void GoBack()
    {
        _mainWindowViewModel.NavigateToMenuPage();
    }
}