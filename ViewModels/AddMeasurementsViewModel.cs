using System;
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
    [ObservableProperty] private string _selectedDate;
    [ObservableProperty] private string? _textField1;
    [ObservableProperty] private string? _textField2;
    [ObservableProperty] private string? _textField3;
    [ObservableProperty] private bool? _isArrhythmiaCheckBox;
    [ObservableProperty] private bool _isError;
    [ObservableProperty] private string _errorMessage;

    // For designer ONLY
    public AddMeasurementsViewModel()
    {
        PageType = EPageType.HeartMeasurement;
        IsError = false;
        IsArrhythmiaCheckBox = false;
        _mainWindowViewModel = null;
    }

    public AddMeasurementsViewModel(MainWindowViewModel mainWindowViewModel)
    {
        PageType = EPageType.HeartMeasurement;
        IsError = false;
        IsArrhythmiaCheckBox = false;
        _mainWindowViewModel = mainWindowViewModel;
    }

    private void ChangePage(EPageType pageType)
    {
        PageType = pageType;
    }


    private void SubmitHeartMeasurement()
    {
        if (string.IsNullOrEmpty(SelectedDate))
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

        if (IsArrhythmiaCheckBox == null)
        {
            var em = "Invalid Heart Measurement: Error in checkbox \"Arrhythmia\"!";
            throw new Exception(em);
        }

        HeartMeasurement hMeasurement = new HeartMeasurement()
        {
            Id = null,
            PersonId = Constants.SelectedPersonId,
            Date = SelectedDate,
            Sys = int.Parse(TextField1),
            Dia = int.Parse(TextField2),
            HRhythm = int.Parse(TextField3),
            IsArrhythmia = IsArrhythmiaCheckBox
        };

        HeartMDao hmdao = new HeartMDao();
        hmdao.InsertRecord(hMeasurement);
    }

    private void SubmitBodyMeasurements()
    {
        if (string.IsNullOrEmpty(SelectedDate))
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
            Date = SelectedDate,
            PersonId = Constants.SelectedPersonId,
            Temperature = double.Parse(TextField1)
        };

        BodyMDao bodyMDao = new BodyMDao();
        bodyMDao.InsertRecord(bodyMeasurement);
    }

    private void SubmitFeelingMeasurements()
    {
        if (string.IsNullOrEmpty(SelectedDate))
        {
            var em = "Invalid Feeling Measurement: Error in field \"DATETIME\"!";
            throw new Exception(em);
        }

        FeelingMeasurement feelingMeasurement = new FeelingMeasurement()
        {
            Id = null,
            PersonId = Constants.SelectedPersonId,
            Date = SelectedDate,
            Medication = string.IsNullOrEmpty(TextField1) ? string.Empty : TextField1,
            Feeling = string.IsNullOrEmpty(TextField2) ? string.Empty : TextField2
        };

        FeelingMDao feelingMDao = new FeelingMDao();
        feelingMDao.InsertRecord(feelingMeasurement);
    }

    [RelayCommand]
    private void BodyMChange()
    {
        ChangePage(EPageType.BodyMeasurement);
    }
    
    [RelayCommand]
    private void HeartMChange()
    {
        ChangePage(EPageType.HeartMeasurement);
    }

    [RelayCommand]
    private void FeelMChange()
    {
        ChangePage(EPageType.FeelingMeasurement);
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
            default:
                SelectedDate = string.Empty;
                TextField1 = string.Empty;
                TextField2 = string.Empty;
                TextField3 = string.Empty;
                IsArrhythmiaCheckBox = false;
                ErrorMessage = string.Empty;
                break;
        }
    }
    
    [RelayCommand]
    private void CloseError() => IsError = false;
}