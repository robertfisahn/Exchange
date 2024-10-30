using UserPortal.Models.Dto;

namespace UserPortal.Interfaces
{
    public interface IBankService
    {
        Task<List<TokenDto>> GetTokensAsync();
        Task<WalletDto> GetWallet();
        Task CreateOrder(CreateOrderDto createOrder); 
    }
}
