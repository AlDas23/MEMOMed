using Avalonia.Controls;
using MEMOMed.ViewModels;

namespace MEMOMed.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
                
        var mainVm = new MainWindowViewModel();
        DataContext = mainVm;

        mainVm.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(MainWindowViewModel.WindowWidth))
                Width = mainVm.WindowWidth;
            if (e.PropertyName == nameof(MainWindowViewModel.WindowHeight))
                Height = mainVm.WindowHeight;
        };
    }
}