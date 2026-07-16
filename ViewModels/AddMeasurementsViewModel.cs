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
    [ObservableProperty] private TimeSpan _selectedTime;
    [ObservableProperty] private string? _textField1;
    [ObservableProperty] private string? _textField1Name;
    [ObservableProperty] private string? _textField2;
    [ObservableProperty] private string? _textField2Name;
    [ObservableProperty] private bool _isTextField2Visible;
    [ObservableProperty] private string? _textField3;
    [ObservableProperty] private string? _textField3Name;
    [ObservableProperty] private bool _isTextField3Visible;
    [ObservableProperty] private string? _textField4;
    [ObservableProperty] private string? _textField4Name;
    [ObservableProperty] private bool _isTextField4Visible;
    [ObservableProperty] private string? _textField5;
    [ObservableProperty] private string? _textField5Name;
    [ObservableProperty] private bool _isTextField5Visible;
    [ObservableProperty] private string? _textField6;
    [ObservableProperty] private string? _textField6Name;
    [ObservableProperty] private bool _isTextField6Visible;
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
        SelectedTime = DateTime.Now.TimeOfDay;
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
        
        var dateTime = $"{SelectedDate:yyyy'/'MM'/'dd} | {SelectedTime:HH:mm}";

        var hMeasurement = new HeartMeasurement(
            Constants.SelectedPersonId!.Value,
            dateTime,
            int.Parse(TextField1),
            int.Parse(TextField2),
            int.Parse(TextField3),
            TextField4,
            TextField5,
            double.Parse(TextField6 ?? string.Empty),
            IsArrhythmiaCheckBox
        );

        var hmdao = new HeartMDao();
        hmdao.InsertRecord(hMeasurement);
    }

    private void SubmitBodyMeasurements() // TO BE REMOVED
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
            Constants.SelectedPersonId!.Value,
            SelectedDate.ToString("yyyy'/'MM'/'dd"),
            double.Parse(TextField1)
        );

        BodyMDao bodyMDao = new BodyMDao();
        bodyMDao.InsertRecord(bodyMeasurement);
    }
    
    [RelayCommand]
    private void BodyMChange() // TO BE REMOVED
    {
        // ChangePage(EPageType.BodyMeasurement);
        // TextField1Name = "Temperature";
        // IsTextField2Visible = false;
        // TextField2Name = string.Empty;
        // IsTextField3Visible = false;
        // TextField3Name = string.Empty;
        // IsTextField4Visible = false;
        // TextField4Name = string.Empty;
        // IsTextField5Visible = false;
        // TextField5Name = string.Empty;
        // IsVisibleArrhythmiaCheckBox = false;
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
        IsTextField4Visible = true;
        TextField4Name = "Feeling";
        IsTextField5Visible = true;
        TextField5Name = "Medication";
        IsTextField6Visible = true;
        TextField6Name = "Temperature";
        IsVisibleArrhythmiaCheckBox = true;
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
        }

        switch (PageType)
        {
            case EPageType.HeartMeasurement:
                try
                {
                    SubmitHeartMeasurement();
                    goto default;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    IsError = true;
                    break;
                }
            default:
                SelectedDate = DateTime.Now.Date;
                TextField1 = string.Empty;
                TextField2 = string.Empty;
                TextField3 = string.Empty;
                TextField4 = string.Empty;
                TextField5 = string.Empty;
                TextField6 = string.Empty;
                IsArrhythmiaCheckBox = false;
                ErrorMessage = string.Empty;
                break;
        }
    }
}