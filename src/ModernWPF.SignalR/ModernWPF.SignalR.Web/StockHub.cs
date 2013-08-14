namespace ModernWPF.SignalR.Web
{
    using Microsoft.AspNet.SignalR;

    using ModernWPF.SignalR.Desktop;

    public class StockHub : Hub
    {
        public void NotifyNewStock(double stockValue)
        {
            this.Clients.All.UpdateStockPrice(stockValue);
        }
    }
}