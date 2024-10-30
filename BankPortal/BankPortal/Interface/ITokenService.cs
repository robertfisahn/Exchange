using BankPortal.Models.Dto;

namespace BankPortal.Interface
{
    public interface ITokenService
    {
        Task<List<TokenDto>> GetTokens();
        Task<TokenDto> GetTokenByName(string name);
        Task UpdateTokenPrice(TokenUpdateDto dto);
        Task<List<TokenDto>> GetWalletTokens(List<string> walletTokens);
    }
}
