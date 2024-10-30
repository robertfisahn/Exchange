using BankPortal.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TokenPortal.Models.Dto
{
    public class TokenUpdateDto
    {
        public string Name { get; set; } = null!;
        public decimal Quantity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }
    }
}
