using AutoMapper;
using BankPortal.Models.Enums;
using MongoDB.Driver;
using TokenPortal.Dto.Models;
using TokenPortal.Entities;
using TokenPortal.Interface;
using TokenPortal.Models.Dto;

namespace TokenPortal.Services
{
    public class TokenService(IMongoDatabase database, IMapper mapper) : ITokenService
    {
        private readonly IMongoCollection<Token> _tokenCollection = database.GetCollection<Token>("Tokens");
        private readonly IMapper _mapper = mapper;

        public async Task<List<TokenDto>> GetTokens()
        {
            var token = await _tokenCollection.Find(token => true).ToListAsync();
            return _mapper.Map<List<TokenDto>>(token);
        }

        public async Task<TokenDto> GetTokenByName(string tokenName)
        {
            var token = await _tokenCollection.Find(t => t.Name == tokenName).FirstOrDefaultAsync() ?? throw new Exception();
            return _mapper.Map<TokenDto>(token);
        }

        public async Task CreateToken(TokenCreateDto dto)
        {
            await _tokenCollection.InsertOneAsync(_mapper.Map<Token>(dto));
        }

        public async Task DeleteToken(string id)
        {
            await _tokenCollection.DeleteOneAsync(t => t.Id == id);
        }

        public async Task UpdateToken(Token token)
        {
            var filter = Builders<Token>.Filter.Eq(t => t.Id, token.Id);
            await _tokenCollection.ReplaceOneAsync(filter, token);
        }

        public async Task Update(TokenUpdateDto dto)
        {
            var token = await _tokenCollection.Find(t => t.Name == dto.Name).FirstOrDefaultAsync() ?? throw new Exception();
            decimal newPrice;

            if (dto.OrderType == OrderType.Buy)
            {
                newPrice = token.Price * (1 + (dto.Quantity / token.TotalSupply));
            }
            else
            {
                newPrice = (token.Price * (1 - (dto.Quantity / token.TotalSupply)));
            }
            
            var filter = Builders<Token>.Filter.Eq(t => t.Id, token.Id);
            var update = Builders<Token>.Update.Set(t => t.Price, newPrice);

            await _tokenCollection.UpdateOneAsync(filter, update);
        }

        public async Task<List<TokenDto>> GetWalletTokens(List<string> tokens)
        {
            var filter = Builders<Token>.Filter.In(t => t.Name, tokens);
            var tokenEntities = await _tokenCollection.Find(filter).ToListAsync();
            return _mapper.Map<List<TokenDto>>(tokenEntities);
        }
    }
}
