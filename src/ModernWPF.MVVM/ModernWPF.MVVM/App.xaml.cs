namespace ModernWPF.MVVM
{
    using System.Data.Entity;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Database.SetInitializer(new MarketInitializer());
        }
    }
}
