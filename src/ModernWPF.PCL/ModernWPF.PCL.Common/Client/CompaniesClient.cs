namespace ModernWPF.PCL.Common.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using ModernWPF.PCL.Common.Model;
    using Newtonsoft.Json;

    public class CompaniesClient
    {
        private const string CompaniesUrl = "/api/companies";

        private readonly Uri serviceBaseUri;

        public CompaniesClient(string serviceBaseUrl)
        {
            this.serviceBaseUri = new Uri(serviceBaseUrl, UriKind.Absolute);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            using (var client = new HttpClient { BaseAddress = this.serviceBaseUri })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(CompaniesUrl);
                var companiesJsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Company>>(companiesJsonResponse);
            }
        }

        public async Task<bool> PostCompany(Company company)
        {
            using (var client = new HttpClient { BaseAddress = this.serviceBaseUri })
            {
                var response = await client.PostAsync(
                    CompaniesUrl,
                    new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }

        }
    }
}
