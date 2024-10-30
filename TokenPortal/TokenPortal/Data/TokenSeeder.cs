using MongoDB.Driver;
using TokenPortal.Entities;

namespace TokenPortal.Data
{
    public class TokenSeeder
    {
        private readonly IMongoCollection<Token> _tokenCollection;

        public TokenSeeder(IMongoDatabase database)
        {
            _tokenCollection = database.GetCollection<Token>("Tokens");
        }

        public void Seed()
        {
            if (!_tokenCollection.Find(_ => true).Any())
            {
                var tokens = new List<Token>
                {
                    new()
                    {
                        Name = "Bitcoin",
                        Price = 65000m,
                        TotalSupply = 19000000m

                    },
                    new()
                    {
                        Name = "Ethereum",
                        Price = 2500m,
                        TotalSupply = 120000000m 
                    },
                    new()
                    {
                        Name = "Binance Coin",
                        Price = 400m,
                        TotalSupply = 160000000m
                    },
                    new()
                    {
                        Name = "Solana",
                        Price = 155m,
                        TotalSupply = 450000000m
                    }
                };

                _tokenCollection.InsertMany(tokens);
            }
        }
    }
}