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

        mainVm.PropertyChanged += (_, e) =>
        {
            switch (e.PropertyName)
            {
                case nameof(MainWindowViewModel.WindowWidth):
                    Width = mainVm.WindowWidth;
                    break;
                case nameof(MainWindowViewModel.WindowHeight):
                    Height = mainVm.WindowHeight;
                    break;
            }
        };
    }
}