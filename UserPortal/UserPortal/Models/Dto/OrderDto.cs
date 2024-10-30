using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserPortal.Models.Enums;

namespace UserPortal.Models.Dto
{
    public class OrderDto
    {
        public string UserEmail { get; set; } = null!;
        public string TokenName { get; set; } = null!;
        public decimal Quantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }
    }
}
