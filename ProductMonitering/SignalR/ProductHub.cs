using Microsoft.AspNetCore.SignalR;

namespace ProductMonitering.SignalR
{
    public class ProductHub : Hub
    {
        public async Task NotifyNewProduct()
        {
            await Clients.All.SendAsync("ReceiveUpdate");
        }
    }
}
