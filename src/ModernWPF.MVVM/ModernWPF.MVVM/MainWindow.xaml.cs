namespace ModernWPF.MVVM
{
    using System.Windows;

    using ModernWPF.MVVM.ViewModels;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
}