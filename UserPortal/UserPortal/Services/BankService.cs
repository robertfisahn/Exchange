using AutoMapper;
using Newtonsoft.Json;
using UserPortal.Exceptions;
using UserPortal.Helpers;
using UserPortal.Interfaces;
using UserPortal.Models.Dto;

namespace UserPortal.Services
{
    public class BankService(HttpClient httpClient, IMapper mapper, HttpResponseHandler httpResponseHandler, IUserContextService userContextService, IConfiguration configuration) : IBankService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMapper _mapper = mapper;
        private readonly HttpResponseHandler _httpResponseHandler = httpResponseHandler;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly string _bankURL = configuration["ApiSettings:BankAPI_URL"];

        public async Task<List<TokenDto>> GetTokensAsync()
        {
            var response = await _httpClient.GetAsync($"{_bankURL}/api/bank/token");
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(responseBody);
            return tokens ?? throw new NotFoundException("Tokens not found");
        }

        public async Task<WalletDto> GetWallet()
        {
            var userEmail = _userContextService.GetUserEmail();
            var response = await _httpClient.GetAsync($"{_bankURL}/api/bank/wallet/{userEmail}");
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
            var responseBody = response.Content.ReadAsStringAsync().Result;
            var wallet = JsonConvert.DeserializeObject<WalletDto>(responseBody);
            return wallet;
        }

        public async Task CreateOrder(CreateOrderDto createOrder)
        {
            var orderDto = _mapper.Map<OrderDto>(createOrder);
            orderDto.UserEmail = _userContextService.GetUserEmail();
            var response = await _httpClient.PostAsJsonAsync($"{_bankURL}/api/bank/order", orderDto);
            if (!response.IsSuccessStatusCode)
            {
                _httpResponseHandler.HandleErrorResponse(response);
            }
        }
    }
}
