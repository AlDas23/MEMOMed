using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MEMOMed.Models.DataClasses;
using MEMOMed.Models.DataClasses.DataAccessObjects;

namespace MEMOMed.ViewModels;

public partial class TableViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    [ObservableProperty] private ObservableCollection<HeartMeasurement>? _heartMeasurements;

    [ObservableProperty] private ObservableCollection<BodyMeasurement>? _bodyMeasurements;

    // [ObservableProperty] private ObservableCollection<FeelingMeasurement>? _feelingMeasurements; TO BE REMOVED
    [ObservableProperty] private ObservableCollection<Medicine>? _medicines;
    [ObservableProperty] private string? _title;
    [ObservableProperty] private bool _isHeartPage;
    [ObservableProperty] private bool _isBodyPage;
    [ObservableProperty] private bool _isFeelingPage;
    [ObservableProperty] private bool _isMedicinePage;

    // For designer ONLY
    // public TableViewModel()
    // {
    //     _mainWindowViewModel = null;
    //     HeartMeasurements =
    //         new ObservableCollection<HeartMeasurement>([new HeartMeasurement(2, 1, "2020-12-12", 120, 80, 75, true)]);
    //     BodyMeasurements =
    //         new ObservableCollection<BodyMeasurement>([new BodyMeasurement(4, 2, "2020-12-12", 36.6)]);
    //     FeelingMeasurements =
    //         new ObservableCollection<FeelingMeasurement>([
    //             new FeelingMeasurement(4, 2, "2020-12-12", "80 pills", "Good")
    //         ]);
    //     HeartPChange();
    // }

    public TableViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        if (Constants.SelectedPersonId is null)
        {
            Console.WriteLine("No Person Selected. Returning to Login...");
            _mainWindowViewModel.NavigateToLoginPage();
        }

        SelectHeartMeasurementCommand = new RelayCommand<HeartMeasurement>(OnSelectRecord);
        SelectBodyMeasurementCommand = new RelayCommand<BodyMeasurement>(OnSelectRecord);

        var medDao = new MedicineDao();
        var heartDao = new HeartMDao();
        var bodyDao = new BodyMDao();

        HeartMeasurements =
            new ObservableCollection<HeartMeasurement>(
                heartDao.GrabRecordsByPersonId(Constants.SelectedPersonId!.Value) ?? []);
        BodyMeasurements =
            new ObservableCollection<BodyMeasurement>(
                bodyDao.GrabRecordsByPersonId(Constants.SelectedPersonId.Value) ?? []);
        Medicines =
            new ObservableCollection<Medicine>(
                medDao.GrabAllEntities());

        HeartPChange();
    }

    public ICommand SelectHeartMeasurementCommand { get; }
    public ICommand SelectBodyMeasurementCommand { get; }

    private void OnSelectRecord(HeartMeasurement record)
    {
        _mainWindowViewModel.NavigateToEditMeasurementsPage(record);
    }

    private void OnSelectRecord(BodyMeasurement record)
    {
        _mainWindowViewModel.NavigateToEditMeasurementsPage(record);
    }

    [RelayCommand]
    private void MedicinePChange()
    {
        Title = "Medicine list";
        IsBodyPage = false;
        IsHeartPage = false;
        IsFeelingPage = false;
        IsMedicinePage = true;
    }

    [RelayCommand]
    private void HeartPChange()
    {
        Title = "Heart Measurements";
        IsBodyPage = false;
        IsHeartPage = true;
        IsFeelingPage = false;
        IsMedicinePage = false;
    }

    [RelayCommand]
    private void BodyPChange()
    {
        Title = "Body Measurements";
        IsBodyPage = true;
        IsHeartPage = false;
        IsFeelingPage = false;
        IsMedicinePage = false;
    }

    // [RelayCommand] TO BE REMOVED
    // private void FeelingPChange()
    // {
    //     Title = "Feeling Measurements";
    //     IsBodyPage = false;
    //     IsHeartPage = false;
    //     IsFeelingPage = true;
    //     IsMedicinePage = false;
    // }

    [RelayCommand]
    private void GoBack()
    {
        _mainWindowViewModel.NavigateToMenuPage();
    }
}