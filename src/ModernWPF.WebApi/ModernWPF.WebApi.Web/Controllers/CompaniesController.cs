namespace ModernWPF.WebApi.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using ModernWPF.WebApi.Web.Models;

    public class CompaniesController : ApiController
    {
        private static List<Company> companies = new List<Company> 
        {
            new Company { Id = 1, Name = "Contoso", StockPrice = 10.0 },
            new Company { Id = 2, Name = "Tailspin", StockPrice = 20.0 },
            new Company { Id = 3, Name = "Litware", StockPrice = 30.0 },
        };

        private static int lastId = 3;

        public IEnumerable<Company> Get()
        {
            return companies;
        }

        public Company Get(int id)
        {
            var company = companies.FirstOrDefault(p => p.Id == id);

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return company;
        }

        public int Post(Company company)
        {
            company.Id = ++lastId;

            companies.Add(company);

            return company.Id;
        }
    }
}