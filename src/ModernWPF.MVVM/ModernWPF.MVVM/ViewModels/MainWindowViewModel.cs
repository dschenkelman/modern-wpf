namespace ModernWPF.MVVM.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using ModernWPF.MVVM.Commands;
    using ModernWPF.MVVM.Services;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ICompaniesService companiesService;

        private readonly IStocksService stocksService;

        private Company selectedCompany;

        private bool stocksLoaded;

        public MainWindowViewModel() : this(new CompaniesService(), new StocksService())
        {
        }

        public MainWindowViewModel(ICompaniesService companiesService, IStocksService stocksService)
        {
            this.StocksLoaded = false;
            this.Companies = new ObservableCollection<Company>();
            this.Stocks = new ObservableCollection<Stock>();
            this.companiesService = companiesService;
            this.stocksService = stocksService;
            this.LoadCommand = new DelegateCommand<object>(this.InitialLoad);
        }

        public bool StocksLoaded
        {
            get
            {
                return this.stocksLoaded;
            }

            set
            {
                if (this.stocksLoaded == value)
                {
                    return;
                }

                this.stocksLoaded = value;
                this.OnPropertyChanged();
            }
        }

        public DelegateCommand<object> LoadCommand { get; set; }

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
                    this.UpdateChart();
                }
            }
        }

        private async Task UpdateChart()
        {
            this.Stocks.Clear();

            this.StocksLoaded = false;

            foreach (var stock in await this.stocksService.GetStocks(this.selectedCompany))
            {
                this.Stocks.Add(stock);
            }

            this.StocksLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async Task InitialLoad()
        {
            this.Companies.Clear();

            foreach (var company in await this.companiesService.GetCompanies())
            {
                this.Companies.Add(company);
            }

            if (this.Companies.Count != 0)
            {
                this.SelectedCompany = this.Companies[0];
            }
        }
    }
}