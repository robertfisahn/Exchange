using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TokenPortal.Entities
{
    public class Token
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal TotalSupply { get; set; }
    }
}

