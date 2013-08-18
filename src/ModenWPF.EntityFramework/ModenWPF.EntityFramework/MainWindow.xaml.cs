namespace ModenWPF.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls.DataVisualization.Charting;

    using ModenWPF.EntityFramework.Annotations;

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Company selectedCompany;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Companies = new ObservableCollection<Company>();
            this.Stocks = new ObservableCollection<Stock>();
            this.DataContext = this;
        }

        public ObservableCollection<Stock> Stocks { get; set; }

        public ObservableCollection<Company> Companies { get; set; }

        public Company SelectedCompany
        {
            get
            {
                return this.selectedCompany;
            }

            set
            {
                if (this.selectedCompany == value)
                {
                    return;
                }

                this.selectedCompany = value;

                this.OnPropertyChanged();

                if (this.selectedCompany != null)
                {
                    this.UpdateChart().ContinueWith(t => this.StockChart.Visibility = Visibility.Visible, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }

        private async Task UpdateChart()
        {
            this.Stocks.Clear();

            using (var context = new MarketContext())
            {
                foreach (var stock in await context.Stocks.Where(s => s.CompanyId == this.SelectedCompany.Id).ToListAsync())
                {
                    this.Stocks.Add(stock);
                }
            }
            
            this.ChangeSeriesLegend();
        }

        private void ChangeSeriesLegend()
        {
            var legendItem = this.StockSeries.LegendItems[0] as LegendItem;

            if (legendItem != null)
            {
                legendItem.Content = this.SelectedCompany.Name;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void LoadClick(object sender, RoutedEventArgs e)
        {
            this.LoadButton.IsEnabled = false;

            await Task.Run((Func<Task>)this.InitialLoad);
        }

        private async Task InitialLoad()
        {
            List<Company> items = null;

            using (var context = new MarketContext())
            {
                var companiesContext = context.Companies;
                
                // takes longer due to first access
                items = await companiesContext.ToListAsync();
            }

            await Dispatcher.InvokeAsync(
            () =>
                {
                    this.Companies.Clear();

                    foreach (var company in items)
                    {
                        this.Companies.Add(company);
                    }

                    if (this.Companies.Count != 0)
                    {
                        this.SelectedCompany = this.Companies[0];
                    }
                });
        }
    }
}