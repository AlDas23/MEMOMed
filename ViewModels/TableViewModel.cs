using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.ViewModels;

public partial class TableViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    [ObservableProperty] private ObservableCollection<HeartMeasurement>? _heartMeasurements;
    [ObservableProperty] private ObservableCollection<BodyMeasurement>? _bodyMeasurements;
    [ObservableProperty] private ObservableCollection<FeelingMeasurement>? _feelingMeasurements;
    [ObservableProperty] private ObservableCollection<Medicine>? _medicines;
    [ObservableProperty] private bool _isHeartPage;
    [ObservableProperty] private bool _isBodyPage;
    [ObservableProperty] private bool _isFeelingPage;
    [ObservableProperty] private bool _isMedicinePage;

    // For designer ONLY
    public TableViewModel()
    {
        _mainWindowViewModel = null;

        HeartPChange();
    }

    public TableViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        if (Constants.SelectedPersonId is null)
        {
            Console.WriteLine("No Person Selected. Returning to Login...");
            _mainWindowViewModel.NavigateToLoginPage();
        }
        var medDao = new MedicineDao();
        var heartDao = new HeartMDao();
        var bodyDao = new BodyMDao();
        var feelDao = new FeelingMDao();

        HeartMeasurements =
            new ObservableCollection<HeartMeasurement>(
                heartDao.GrabRecordsByPersonId(Constants.SelectedPersonId.Value));
        BodyMeasurements =
            new ObservableCollection<BodyMeasurement>(bodyDao.GrabRecordsByPersonId(Constants.SelectedPersonId.Value) ??
                                                      new List<BodyMeasurement>());
        FeelingMeasurements =
            new ObservableCollection<FeelingMeasurement>(
                feelDao.GrabRecordsByPersonId(Constants.SelectedPersonId.Value) ?? new List<FeelingMeasurement>());
        Medicines = new ObservableCollection<Medicine>(medDao.GrabAllEntities() ?? new List<Medicine>());
        
        HeartPChange();
    }


    [RelayCommand]
    private void MedicinePChange()
    {
        IsBodyPage = false;
        IsHeartPage = false;
        IsFeelingPage = false;
        IsMedicinePage = true;
    }

    [RelayCommand]
    private void HeartPChange()
    {
        IsBodyPage = false;
        IsHeartPage = true;
        IsFeelingPage = false;
        IsMedicinePage = false;
    }

    [RelayCommand]
    private void BodyPChange()
    {
        IsBodyPage = true;
        IsHeartPage = false;
        IsFeelingPage = false;
        IsMedicinePage = false;
    }

    [RelayCommand]
    private void FeelingPChange()
    {
        IsBodyPage = false;
        IsHeartPage = false;
        IsFeelingPage = true;
        IsMedicinePage = false;
    }

    [RelayCommand]
    private void GoBack()
    {
        _mainWindowViewModel.NavigateToMenuPage();
    }
}