namespace ModernWPF.SignalR.Desktop
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.DataVisualization.Charting;

    using Microsoft.AspNet.SignalR.Client.Hubs;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TimedStock> timedPrices;

        private HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
        }

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.PopulateInitialData();

            this.ChangeSeriesLegend();

            this.PriceSeries.ItemsSource = this.timedPrices;

            this.connection = new HubConnection("http://localhost:2481/signalr", useDefaultUrl: false);

            IHubProxy stockTickerHubProxy = this.connection.CreateHubProxy("StockHub");
            stockTickerHubProxy.On<double>("UpdateStockPrice", this.OnUpdatedStockPrice);
            await this.connection.Start();
        }

        private async void OnUpdatedStockPrice(double price)
        {
            await Dispatcher.BeginInvoke(new Action(() => this.timedPrices.Add(new TimedStock { Price = price, Time = DateTime.Now })));
        }

        private void ChangeSeriesLegend()
        {
            var legendItem = this.PriceSeries.LegendItems[0] as LegendItem;

            if (legendItem != null)
            {
                legendItem.Content = "Tailspin";
            }
        }

        private void PopulateInitialData()
        {
            this.timedPrices = new ObservableCollection<TimedStock>
                                   {
                                       new TimedStock
                                           {
                                               Price = 10.0,
                                               Time = DateTime.Now.AddMinutes(-30)
                                           },
                                       new TimedStock()
                                           {
                                               Price = 10.5,
                                               Time = DateTime.Now.AddMinutes(-10)
                                           },
                                       new TimedStock()
                                           {
                                               Price = 10.4,
                                               Time = DateTime.Now.AddMinutes(-5)
                                           }
                                   };
        }
    }
}
