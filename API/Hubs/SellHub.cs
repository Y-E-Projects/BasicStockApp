using DTO.Models;
using Microsoft.AspNetCore.SignalR;
using BL.Abstract;

namespace API.Hubs
{
    public class SellHub : Hub
    {
        private readonly ISellService _sellService;

        public SellHub(ISellService sellService)
        {
            _sellService = sellService;
        }

        public async Task BroadcastSell(ListModel.Sell sell)
        {
            await Clients.All.SendAsync("ReceiveSellUpdate", sell);
        }

        public async Task GetList()
        {
            var sells = _sellService.GetList();
            await Clients.Caller.SendAsync("ReceiveSellList", sells);
        }
    }
}
