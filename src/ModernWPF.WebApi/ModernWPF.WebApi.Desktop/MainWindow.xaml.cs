namespace ModernWPF.WebApi.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Windows;

    using ModernWPF.WebApi.Web.Models;

    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Company> companies;

        private const string BaseUrl = "http://localhost:2044/";

        private const string CompaniesUrl = "/api/companies";

        public MainWindow()
        {
            this.InitializeComponent();
            this.companies = new ObservableCollection<Company>();
            this.ProductsList.ItemsSource = this.companies;
        }

        private async void GetCompanies(object sender, RoutedEventArgs e)
        {
            this.GetButton.IsEnabled = false;
            this.companies.Clear();

            var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            var response = await client.GetAsync(CompaniesUrl);

            foreach (var company in await response.Content.ReadAsAsync<IEnumerable<Company>>())
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

            var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            var company = new Company
            {
                Name = this.CompanyTextBox.Text,
                StockPrice = price
            };

            await client.PostAsJsonAsync(CompaniesUrl, company);

            this.AddButton.IsEnabled = true;
        }
    }
}