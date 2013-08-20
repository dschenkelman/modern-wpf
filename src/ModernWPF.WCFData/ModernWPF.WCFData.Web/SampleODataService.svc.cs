namespace ModernWPF.WCFData.Web
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using ModernWPF.WCFData.Web.Models;

    public class SampleODataService : DataService<SampleDatabaseContext>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Employees", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
