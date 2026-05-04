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
    [ObservableProperty] private DateTimeOffset _selectedDate;
    [ObservableProperty] private string? _textField1;
    [ObservableProperty] private string? _textField1Name;
    [ObservableProperty] private string? _textField2;
    [ObservableProperty] private string? _textField2Name;
    [ObservableProperty] private bool _isTextField2Visible;
    [ObservableProperty] private string? _textField3;
    [ObservableProperty] private string? _textField3Name;
    [ObservableProperty] private bool _isTextField3Visible;
    [ObservableProperty] private bool _isArrhythmiaCheckBox;
    [ObservableProperty] private bool _isVisibleArrhythmiaCheckBox;
    [ObservableProperty] private bool _isError;
    [ObservableProperty] private string? _errorMessage;

    // For designer ONLY
    // public AddMeasurementsViewModel()
    // {
    //     HeartMChange();
    //     SelectedDate = DateTime.Now.Date;
    //     IsError = false;
    //     IsArrhythmiaCheckBox = false;
    //     _mainWindowViewModel = null;
    // }

    public AddMeasurementsViewModel(MainWindowViewModel mainWindowViewModel)
    {
        HeartMChange();
        SelectedDate = DateTime.Now.Date;
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
        if (string.IsNullOrEmpty(SelectedDate.ToString()) || !DateTime.TryParse(SelectedDate.ToString(), out _))
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

        HeartMeasurement hMeasurement = new HeartMeasurement(
            Constants.SelectedPersonId.Value,
            SelectedDate.ToString("yyyy'/'MM'/'dd"),
            int.Parse(TextField1),
            int.Parse(TextField2),
            int.Parse(TextField3),
            IsArrhythmiaCheckBox
        );

        HeartMDao hmdao = new HeartMDao();
        hmdao.InsertRecord(hMeasurement);
    }

    private void SubmitBodyMeasurements()
    {
        if (string.IsNullOrEmpty(SelectedDate.ToString()) || !DateTime.TryParse(SelectedDate.ToString(), out _))
        {
            var em = "Invalid Body Measurement: Error in field \"DATETIME\"!";
            throw new Exception(em);
        }

        if (string.IsNullOrEmpty(TextField1) || !double.TryParse(TextField1, out _))
        {
            var em = "Invalid Body Measurement: Error in field \"TEMPERATURE\"!";
            throw new Exception(em);
        }

        BodyMeasurement bodyMeasurement = new BodyMeasurement(
            Constants.SelectedPersonId.Value,
            SelectedDate.ToString("yyyy'/'MM'/'dd"),
            double.Parse(TextField1)
        );

        BodyMDao bodyMDao = new BodyMDao();
        bodyMDao.InsertRecord(bodyMeasurement);
    }

    private void SubmitFeelingMeasurements()
    {
        if (string.IsNullOrEmpty(SelectedDate.ToString()) || !DateTime.TryParse(SelectedDate.ToString(), out _))
        {
            var em = "Invalid Feeling Measurement: Error in field \"DATETIME\"!";
            throw new Exception(em);
        }

        FeelingMeasurement feelingMeasurement = new FeelingMeasurement(
            Constants.SelectedPersonId.Value,
            SelectedDate.ToString("yyyy'/'MM'/'dd"),
            TextField1,
            TextField2
        );

        FeelingMDao feelingMDao = new FeelingMDao();
        feelingMDao.InsertRecord(feelingMeasurement);
    }

    [RelayCommand]
    private void BodyMChange()
    {
        ChangePage(EPageType.BodyMeasurement);
        TextField1Name = "Temperature";
        IsTextField2Visible = false;
        TextField2Name = " ";
        IsTextField3Visible = false;
        TextField2Name = " ";
        IsVisibleArrhythmiaCheckBox = false;
    }

    [RelayCommand]
    private void HeartMChange()
    {
        ChangePage(EPageType.HeartMeasurement);
        TextField1Name = "Sys";
        IsTextField2Visible = true;
        TextField2Name = "Dia";
        IsTextField3Visible = true;
        TextField3Name = "Heart rate";
        IsVisibleArrhythmiaCheckBox = true;
    }

    [RelayCommand]
    private void FeelMChange()
    {
        ChangePage(EPageType.FeelingMeasurement);
        TextField1Name = "Medication";
        IsTextField2Visible = true;
        TextField2Name = "Feeling";
        IsTextField3Visible = false;
        TextField3Name = " ";
        IsVisibleArrhythmiaCheckBox = false;
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

        if (IsError)
        {
            IsError = false;
            return;
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
                SelectedDate = DateTime.Now.Date;
                TextField1 = string.Empty;
                TextField2 = string.Empty;
                TextField3 = string.Empty;
                IsArrhythmiaCheckBox = false;
                ErrorMessage = string.Empty;
                break;
        }
    }
}