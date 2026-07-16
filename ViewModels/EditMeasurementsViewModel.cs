using System;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.ViewModels;

public partial class EditMeasurementsViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly int _oldId;
    private readonly EPageType _pageType;
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

    public EditMeasurementsViewModel(MainWindowViewModel mainWindowViewModel, HeartMeasurement heartMeasurement)
    {
        _pageType = EPageType.HeartMeasurement;
        HeartPageInit();
        IsError = false;
        _mainWindowViewModel = mainWindowViewModel;
        _oldId = heartMeasurement.Id!.Value;

        var parts = heartMeasurement.DateTime!.Split([" | "], StringSplitOptions.None);
        var datePart = parts[0].Trim();
        var timePart = parts[1].Trim();

        // Parse the date part (yyyy/MM/dd)
        if (!DateTime.TryParseExact(datePart, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime parsedDate))
        {
            throw new FormatException($"Unable to parse date part: '{datePart}'. Expected format: yyyy/MM/dd");
        }

        // Parse the time part (HH:mm)
        if (!TimeSpan.TryParseExact(timePart, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan parsedTime))
        {
            throw new FormatException($"Unable to parse time part: '{timePart}'. Expected format: HH:mm");
        }

        // Populate fields with received data
        SelectedDate = parsedDate;
        SelectedTime = parsedTime;
        TextField1 = heartMeasurement.Sys.ToString();
        TextField2 = heartMeasurement.Dia.ToString();
        TextField3 = heartMeasurement.HRhythm.ToString();
        TextField4 = heartMeasurement.Feeling;
        TextField5 = heartMeasurement.Medication;
        TextField6 = heartMeasurement.Temperature.ToString();
        IsArrhythmiaCheckBox = heartMeasurement.IsArrhythmia;
    }

    private void BodyPageInit() // TO BE REMOVED
    {
        TextField1Name = "Temperature";
        IsTextField2Visible = false;
        TextField2Name = string.Empty;
        IsTextField3Visible = false;
        TextField3Name = string.Empty;
        IsTextField4Visible = false;
        TextField4Name = string.Empty;
        IsTextField5Visible = false;
        TextField5Name = string.Empty;
        IsVisibleArrhythmiaCheckBox = false;
    }

    private void HeartPageInit()
    {
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
    

    private void SubmitEditBodyMeasurement() // TO BE REMOVED
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

        var bodyMeasurement = new BodyMeasurement(
            Constants.SelectedPersonId!.Value,
            SelectedDate.ToString("yyyy'/'MM'/'dd"),
            double.Parse(TextField1)
        );

        var bodyMDao = new BodyMDao();
        bodyMDao.UpdateRecord(bodyMeasurement, _oldId);
    }

    private void SubmitEditHeartMeasurement()
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

        var heartMDao = new HeartMDao();
        heartMDao.UpdateRecord(hMeasurement, _oldId);
    }
    
    private void DeleteBodyMeasurement() // TO BE REMOVED
    {
        var bodyMDao = new BodyMDao();
        bodyMDao.DeleteRecord(_oldId);
    }

    private void DeleteHeartMeasurement()
    {
        var heartMDao = new HeartMDao();
        heartMDao.DeleteRecord(_oldId);
    }

    [RelayCommand]
    private void Edit()
    {
        try
        {
            switch (_pageType)
            {
                case EPageType.HeartMeasurement:
                    SubmitEditHeartMeasurement();
                    break;
            }

            _mainWindowViewModel.NavigateToTablePage();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            IsError = true;
        }
    }

    [RelayCommand]
    private void Delete()
    {
        try
        {
            switch (_pageType)
            {
                case EPageType.HeartMeasurement:
                    DeleteHeartMeasurement();
                    break;
            }

            _mainWindowViewModel.NavigateToTablePage();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            IsError = true;
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        _mainWindowViewModel.NavigateToTablePage();
    }
}