using CommunityToolkit.Mvvm.ComponentModel;
using MEMOMed.Models.DataClasses;

namespace MEMOMed.ViewModels.DataWrapperVM;

public partial class MedicineWrapperViewModel : ViewModelBase
{
    public Medicine Medicine { get; }
    [ObservableProperty] private bool _isSelected;

    public MedicineWrapperViewModel(Medicine medicine)
    {
        Medicine = medicine;
    }
}