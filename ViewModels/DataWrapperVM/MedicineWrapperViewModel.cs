using CommunityToolkit.Mvvm.ComponentModel;
using MEMOMed.Models.DataClasses;

namespace MEMOMed.ViewModels.DataWrapperVM;

public partial class MedicineWrapperViewModel(Medicine medicine, bool isSelected = false) : ViewModelBase
{
    public Medicine Medicine { get; } = medicine;
    [ObservableProperty] private bool _isSelected = isSelected;
}