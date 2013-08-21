namespace ModernWPF.PCL.Desktop
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using ModernWPF.PCL.Common.Client;
    using ModernWPF.PCL.Common.Model;

    public partial class MainWindow : Window
    {
        private const string BaseUrl = "http://localhost:2046/";

        private readonly CompaniesClient client;
        private readonly ObservableCollection<Company> companies;

        public MainWindow()
        {
            this.InitializeComponent();

            this.client = new CompaniesClient(BaseUrl);
            this.companies = new ObservableCollection<Company>();
            this.ProductsList.ItemsSource = this.companies;
        }

        private async void GetCompanies(object sender, RoutedEventArgs e)
        {
            this.GetButton.IsEnabled = false;
            this.companies.Clear();

            foreach (var company in await this.client.GetCompaniesAsync())
            {
                this.companies.Add(company);
            }

            this.GetButton.IsEnabled = true;
        }

        private async void AddCompany(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.CompanyTextBox.Text))
            {
                MessageBox.Show("La empresa no tiene nombre.");
                return;
            }

            double price;

            if (!double.TryParse(this.PriceTextBox.Text, out price))
            {
                MessageBox.Show("El precio no es valido.");
                return;
            }

            this.AddButton.IsEnabled = false;

            var company = new Company
            {
                Name = this.CompanyTextBox.Text,
                StockPrice = price
            };

            await this.client.PostCompany(company);

            this.AddButton.IsEnabled = true;
        }
    }
}