using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BankPortal.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        [EnumMember(Value = "Buy")]
        Buy,
        [EnumMember(Value = "Sell")]
        Sell
    }
}
