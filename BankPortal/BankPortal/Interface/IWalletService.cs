using BankPortal.Entities;
using BankPortal.Models.Dto;

namespace BankPortal.Interface
{
    public interface IWalletService
    {
        Task <List<Wallet>> GetWallets();
        Task <Wallet> GetWallet(string userEmail);
        Task<WalletDto> GetOrCreateWalletDto(string userEmail);
        Task <Wallet> Create(string userEmail);
        Task AddOrUpdateToken(OrderDto order);
        Task RemoveOrUpdateToken(OrderDto order);
    }
}
