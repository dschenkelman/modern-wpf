namespace ModernWPF.MVVM.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public interface ICompaniesService
    {
        Task<IEnumerable<Company>> GetCompanies();
    }
    
    public class CompaniesService : ICompaniesService
    {
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            using (var context = new MarketContext())
            {
                return await context.Companies.ToListAsync();
            }
        }
    }
}
