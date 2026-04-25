using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.ViewModels;

public partial class MedicineConfigurationViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    [ObservableProperty] private EPageType _pageType;
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
    [ObservableProperty] private bool _isError;
    [ObservableProperty] private string _errorMessage;


    private void ChangePage(EPageType pageType)
    {
        PageType = pageType;
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

        Medicine medicine = new Medicine()
        {
            Id = null,
            Name = NameField,
            Description = DescriptionField,
            DaySchedule = daySchedule,
            TimeSchedule = timeSchedule
        };
        MedicineDao medicineDao = new MedicineDao();
        medicineDao.InsertEntity(medicine);
    }

    private void SubmitMedicineAssign()
    {
        //TODO Implement medicine assignment page. User should be able to assign which medicine (from all) is assigned to him.
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
}