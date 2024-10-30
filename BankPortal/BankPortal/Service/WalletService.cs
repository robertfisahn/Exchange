using AutoMapper;
using BankPortal.Entities;
using BankPortal.Exceptions;
using BankPortal.Interface;
using BankPortal.Models.Dto;
using MongoDB.Driver;

namespace BankPortal.Service
{
    public class WalletService(IMongoDatabase database, ITokenService tokenService, IMapper mapper) : IWalletService
    {
        private readonly IMongoCollection<Wallet> _walletCollection = database.GetCollection<Wallet>("Wallets");
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;

        public async Task<List<Wallet>> GetWallets()
        {
            return await _walletCollection.Find(wallet => true).ToListAsync();
        }

        public async Task<Wallet> GetWallet(string userEmail)
        {
            return await _walletCollection.Find(wallet => wallet.UserEmail == userEmail).FirstOrDefaultAsync() ??
                throw new NotFoundException("Wallet not found");
        }

        public async Task<Wallet> Create(string userEmail)
        {
            var wallet = new Wallet { UserEmail = userEmail};
            await _walletCollection.InsertOneAsync(wallet);
            return wallet;
        }
        public async Task<Wallet> GetOrCreateWallet(string userEmail)
        {
            var wallet = await _walletCollection.Find(wallet => wallet.UserEmail == userEmail).FirstOrDefaultAsync() ??
                await Create(userEmail);

            return wallet;
        }

        public async Task<WalletDto> GetOrCreateWalletDto(string userEmail)
        {
            var wallet = await _walletCollection.Find(wallet => wallet.UserEmail == userEmail).FirstOrDefaultAsync();
            if (wallet == null) {
                wallet = await Create(userEmail);
            }
            else {
                var walletDto = _mapper.Map<WalletDto>(wallet);
                return await UpdateWalletWithTokenData(walletDto);
            }
            var dto = _mapper.Map<WalletDto>(wallet);
            return dto;
        }

        private async Task<WalletDto> UpdateWalletWithTokenData(WalletDto wallet)
        {
            var tokenNames = wallet.Tokens.Select(t => t.Name).Distinct().ToList();

            var tokenData = await _tokenService.GetWalletTokens(tokenNames);

            foreach (var walletToken in wallet.Tokens)
            {
                var updatedToken = tokenData.FirstOrDefault(t => t.Name == walletToken.Name);
                _mapper.Map(updatedToken, walletToken);
            }

            return wallet;
        }

        public async Task AddOrUpdateToken(OrderDto order)
        {
            var wallet = await GetOrCreateWallet(order.UserEmail);
            wallet.Tokens ??= new List<WalletTokenDto>();
            var existingToken = wallet.Tokens.FirstOrDefault(token => token.Name == order.TokenName);

            if (existingToken == null) {
                wallet.Tokens.Add( new WalletTokenDto { Name = order.TokenName, Quantity = order.Quantity });
            }
            else {
                existingToken.Quantity += order.Quantity;
            }
            var orderDto = _mapper.Map<TokenUpdateDto>(order);
            await _tokenService.UpdateTokenPrice(orderDto);
            await _walletCollection.ReplaceOneAsync(w => w.Id == wallet.Id, wallet);
        }

        public async Task RemoveOrUpdateToken(OrderDto order)
        {
            var wallet = await _walletCollection.Find(wallet => wallet.UserEmail == order.UserEmail).FirstOrDefaultAsync();

            if (wallet.Tokens != null)
            {
                var existingToken = wallet.Tokens.FirstOrDefault(token => token.Name == order.TokenName)
                    ?? throw new NotFoundException("Token not found");

                if (order.Quantity > existingToken.Quantity)
                {
                    throw new BadRequestException("Invalid Quantity");
                }

                existingToken.Quantity -= order.Quantity;

                if(existingToken.Quantity == 0)
                {
                    wallet.Tokens.Remove(existingToken);
                }

                var orderDto = _mapper.Map<TokenUpdateDto>(order);
                await _tokenService.UpdateTokenPrice(orderDto);
                await _walletCollection.ReplaceOneAsync(w => w.Id == wallet.Id, wallet);
            }
            else
            {
                throw new BadRequestException("Empty wallet");
            }
        }
    }
}
