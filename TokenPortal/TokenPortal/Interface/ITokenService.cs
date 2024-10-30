using TokenPortal.Dto.Models;
using TokenPortal.Entities;
using TokenPortal.Models.Dto;

namespace TokenPortal.Interface
{
    public interface ITokenService
    {
        Task<List<TokenDto>> GetTokens();
        Task<TokenDto> GetTokenByName(string tokeName);
        Task CreateToken(TokenCreateDto token);
        Task DeleteToken(string id);
        Task UpdateToken(Token token);
        Task Update(TokenUpdateDto updateTokenDto);
        Task<List<TokenDto>> GetWalletTokens(List<string> tokens);
    }
}
