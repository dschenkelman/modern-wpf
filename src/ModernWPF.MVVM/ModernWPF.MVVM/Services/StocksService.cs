using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWPF.MVVM.Services
{
    using System.Data.Entity;

    public interface IStocksService
    {
        Task<IEnumerable<Stock>> GetStocks(Company company);
    }

    public class StocksService : IStocksService
    {
        public async Task<IEnumerable<Stock>> GetStocks(Company company)
        {
            using (var context = new MarketContext())
            {
                return await context.Stocks.Where(s => s.CompanyId == company.Id).ToListAsync();
            }
        }
    }
}
