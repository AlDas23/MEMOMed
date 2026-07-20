using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.ViewModels;

public partial class EditMedicineViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly int _oldMedicineId;
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
    [ObservableProperty] private string? _errorMessage;

    public EditMedicineViewModel(MainWindowViewModel mainWindowViewModel, Medicine medicine)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _oldMedicineId = medicine.Id!.Value;
        NameField = medicine.Name;
        DescriptionField = medicine.Description;

        var timeScheduleString = medicine.GetTimeScheduleString();
        var splitTimeString = timeScheduleString.Split('|');
        foreach (var time in splitTimeString)
        {
            switch (time)
            {
                case "1":
                    IsMorningSelected = true;
                    break;
                case "2":
                    IsAfternoonSelected = true;
                    break;
                case "3":
                    IsEveningSelected = true;
                    break;
                case "4":
                    IsNightSelected = true;
                    break;
            }
        }

        var dayScheduleString = medicine.GetDayScheduleString();
        var splitDayString = dayScheduleString.Split('|');
        foreach (var day in splitDayString)
        {
            switch (day)
            {
                case "1":
                    IsMondaySelected = true;
                    break;
                case "2":
                    IsTuesdaySelected = true;
                    break;
                case "3":
                    IsWednesdaySelected = true;
                    break;
                case "4":
                    IsThursdaySelected = true;
                    break;
                case "5":
                    IsFridaySelected = true;
                    break;
                case "6":
                    IsSaturdaySelected = true;
                    break;
                case "7":
                    IsSundaySelected = true;
                    break;
            }
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
    
    private void EditMedicine()
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

        var medicine = new Medicine(null, NameField, DescriptionField, daySchedule, timeSchedule);
        var medicineDao = new MedicineDao();
        medicineDao.UpdateEntity(medicine, _oldMedicineId);
    }

    private void DeleteMedicine()
    {
        var medicineDao = new MedicineDao();
        medicineDao.DeleteEntity(_oldMedicineId);
    }

    [RelayCommand]
    private void SubmitEditMedicine()
    {
        if (IsError)
        {
            IsError = false;
        }

        try
        {
            EditMedicine();
            _mainWindowViewModel.NavigateToTablePage();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            IsError = true;
        }
    }
    
    [RelayCommand]
    private void SubmitDeleteMedicine()
    {
        if (IsError)
        {
            IsError = false;
        }

        try
        {
            DeleteMedicine();
            _mainWindowViewModel.NavigateToTablePage();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            IsError = true;
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        _mainWindowViewModel.NavigateToTablePage();
    }
}