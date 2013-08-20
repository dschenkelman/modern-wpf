namespace ModernWPF.WCFData.Web.Models
{
    using System.Data.Entity;
    using ModernWPF.WCFData.Web.Models.Mapping;

    public partial class SampleDatabaseContext : DbContext
    {
        static SampleDatabaseContext()
        {
            Database.SetInitializer<SampleDatabaseContext>(null);
        }

        public SampleDatabaseContext()
            : base("Name=SampleDatabaseContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMap());
        }
    }
}
