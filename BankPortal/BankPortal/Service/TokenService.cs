using BankPortal.Exceptions;
using BankPortal.Helpers;
using BankPortal.Interface;
using BankPortal.Models.Dto;
using Newtonsoft.Json;

namespace BankPortal.Service
{
    public class TokenService(HttpClient httpClient, HttpResponseHandler httpResponseHandler, IConfiguration configuration) : ITokenService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly HttpResponseHandler _httpResponseHandler = httpResponseHandler;
        private readonly string _tokenURL = configuration["ApiSettings:TokenAPI_URL"];

        public async Task<List<TokenDto>> GetTokens()
        {
            var response = await _httpClient.GetAsync($"{_tokenURL}/api/token");
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(responseBody);
            return tokens ?? throw new NotFoundException("Tokens not found");
        }

        public async Task<TokenDto> GetTokenByName(string name)
        {
            var response = await _httpClient.GetAsync($"{_tokenURL}/api/token/{name}");
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenDto>(responseBody);
            return token ?? throw new NotFoundException("Token not found");
        }

        public async Task<List<TokenDto>> GetWalletTokens(List<string> walletTokens)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_tokenURL}/api/token/bulk", walletTokens);
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(responseBody);
            return tokens ?? throw new NotFoundException("Tokens not found");
        }

        public async Task UpdateTokenPrice(TokenUpdateDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_tokenURL}/api/token/update-price", dto);
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
        }
    }
}
