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
    }
}