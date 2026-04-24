using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.ViewModels;

public partial class AddMeasurementsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    [ObservableProperty] private EPageType _pageType;
    [ObservableProperty] private string _datetime;
    [ObservableProperty] private string? _textField1;
    [ObservableProperty] private string? _textField2;
    [ObservableProperty] private string? _textField3;
    [ObservableProperty] private bool? _arrhythmiaCheckBox;
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

    // For designer ONLY
    public AddMeasurementsViewModel()
    {
        PageType = EPageType.HeartMeasurement;
        IsError = false;
        ArrhythmiaCheckBox = false;
        _mainWindowViewModel = null;
    }

    public AddMeasurementsViewModel(MainWindowViewModel mainWindowViewModel)
    {
        PageType = EPageType.HeartMeasurement;
        IsError = false;
        ArrhythmiaCheckBox = false;
        _mainWindowViewModel = mainWindowViewModel;
    }

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

    private void SubmitHeartMeasurement()
    {
        if (string.IsNullOrEmpty(Datetime))
        {
            var em = "Invalid Heart Measurement: Error in field \"DATETIME\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(TextField1) || !int.TryParse(TextField1, out _))
        {
            var em = "Invalid Heart Measurement: Error in field \"SYS\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(TextField2) || !int.TryParse(TextField2, out _))
        {
            var em = "Invalid Heart Measurement: Error in field \"DIA\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(TextField3) || !int.TryParse(TextField3, out _))
        {
            var em = "Invalid Heart Measurement: Error in field \"Heart Rhythm\"!";
            throw new Exception(em);
        }

        if (ArrhythmiaCheckBox == null)
        {
            var em = "Invalid Heart Measurement: Error in checkbox \"Arrhythmia\"!";
            throw new Exception(em);
        }

        HeartMeasurement hMeasurement = new HeartMeasurement()
        {
            Id = null,
            PersonId = Constants.SelectedPersonId,
            Datetime = Datetime,
            Sys = int.Parse(TextField1),
            Dia = int.Parse(TextField2),
            HRhythm = int.Parse(TextField3),
            IsArrhythmia = ArrhythmiaCheckBox
        };

        HeartMDao hmdao = new HeartMDao();
        hmdao.InsertRecord(hMeasurement);
    }

    private void SubmitBodyMeasurements()
    {
        if (string.IsNullOrEmpty(Datetime))
        {
            var em = "Invalid Body Measurement: Error in field \"DATETIME\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(TextField1) || !double.TryParse(TextField1, out _))
        {
            var em = "Invalid Body Measurement: Error in field \"TEMPERATURE\"!";
            throw new Exception(em);
        }

        BodyMeasurement bodyMeasurement = new BodyMeasurement()
        {
            Id = null,
            Datetime = Datetime,
            PersonId = Constants.SelectedPersonId,
            Temperature = double.Parse(TextField1)
        };

        BodyMDao bodyMDao = new BodyMDao();
        bodyMDao.InsertRecord(bodyMeasurement);
    }

    private void SubmitFeelingMeasurements()
    {
        if (string.IsNullOrEmpty(Datetime))
        {
            var em = "Invalid Feeling Measurement: Error in field \"DATETIME\"!";
            throw new Exception(em);
        }

        FeelingMeasurement feelingMeasurement = new FeelingMeasurement()
        {
            Id = null,
            PersonId = Constants.SelectedPersonId,
            Datetime = Datetime,
            Medication = string.IsNullOrEmpty(TextField1) ? string.Empty : TextField1,
            Feeling = string.IsNullOrEmpty(TextField2) ? string.Empty : TextField2
        };

        FeelingMDao feelingMDao = new FeelingMDao();
        feelingMDao.InsertRecord(feelingMeasurement);
    }

    private void SubmitMedicine()
    {
        if (string.IsNullOrEmpty(TextField1))
        {
            var em = "Invalid Medicine Data: Error in field \"NAME\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(TextField2))
        {
            var em = "Invalid Medicine Data: Error in field \"DESCRIPTION\"!";
            throw new Exception(em);
        }

        var timeSchedule = TimeSchedule;
        var daySchedule = DaySchedule;

        Medicine medicine = new Medicine()
        {
            Id = null,
            Name = TextField1,
            Description = TextField2,
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
    private void BodyMChange()
    {
        ChangePage(EPageType.BodyMeasurement);
    }

    [RelayCommand]
    private void FeelMChange()
    {
        ChangePage(EPageType.FeelingMeasurement);
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

    [RelayCommand]
    private void SubmitRecord()
    {
        if (Constants.SelectedPersonId == null)
        {
            Console.WriteLine("No Person Selected. Returning to Login page...");
            _mainWindowViewModel.NavigateToLoginPage();
        }

        switch (PageType)
        {
            case EPageType.HeartMeasurement:
                try
                {
                    SubmitHeartMeasurement();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                }

                goto default;
            case EPageType.BodyMeasurement:
                try
                {
                    SubmitBodyMeasurements();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                }

                goto default;
            case EPageType.FeelingMeasurement:
                try
                {
                    SubmitFeelingMeasurements();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                }

                goto default;
            case EPageType.Medicine:
                try
                {
                    SubmitMedicine();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                }

                goto default;
            case EPageType.MedicineAssign:
                try
                {
                    SubmitMedicineAssign();
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                }

                goto default;
            default:
                Datetime = string.Empty;
                TextField1 = string.Empty;
                TextField2 = string.Empty;
                TextField3 = string.Empty;
                ArrhythmiaCheckBox = false;
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
}