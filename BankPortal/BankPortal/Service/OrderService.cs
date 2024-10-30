using BankPortal.Interface;
using BankPortal.Models.Dto;

namespace BankPortal.Service
{
    public class OrderService(IWalletService walletService) : IOrderService
    {
        private readonly IWalletService _walletService = walletService;

        public async Task ProcessOrder(OrderDto order)
        {
            if(order.OrderType == Models.Enums.OrderType.Buy)
            {
                await _walletService.AddOrUpdateToken(order);
            }
            else if(order.OrderType == Models.Enums.OrderType.Sell)
            {
                await _walletService.RemoveOrUpdateToken(order);
            }    
        }
    }
}
