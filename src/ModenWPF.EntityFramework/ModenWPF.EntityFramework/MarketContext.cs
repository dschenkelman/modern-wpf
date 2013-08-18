namespace ModenWPF.EntityFramework
{
    using System;
    using System.Data.Entity;

    public class MarketContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Stock> Stocks { get; set; }
    }

    public class MarketInitializer : CreateDatabaseIfNotExists<MarketContext>
    {
        protected override void Seed(MarketContext context)
        {
            base.Seed(context);

            var tailspin = new Company { Name = "Tailspin" };

            var contoso = new Company { Name = "Contoso" };

            context.Companies.Add(tailspin);
            context.Companies.Add(contoso);

            context.SaveChanges();

            context.Stocks.Add(new Stock { CompanyId = tailspin.Id, Date = DateTime.Today.AddDays(-3), Price = 10.0 });
            context.Stocks.Add(new Stock { CompanyId = tailspin.Id, Date = DateTime.Today.AddDays(-2), Price = 11.0 });
            context.Stocks.Add(new Stock { CompanyId = tailspin.Id, Date = DateTime.Today.AddDays(-1), Price = 12.0 });
            context.Stocks.Add(new Stock { CompanyId = tailspin.Id, Date = DateTime.Today, Price = 12.5 });

            context.Stocks.Add(new Stock { CompanyId = contoso.Id, Date = DateTime.Today.AddDays(-3), Price = 100.0 });
            context.Stocks.Add(new Stock { CompanyId = contoso.Id, Date = DateTime.Today.AddDays(-2), Price = 99.9 });
            context.Stocks.Add(new Stock { CompanyId = contoso.Id, Date = DateTime.Today.AddDays(-1), Price = 100.2 });
            context.Stocks.Add(new Stock { CompanyId = contoso.Id, Date = DateTime.Today, Price = 98.0 });

            context.SaveChanges();
        }
    }
}