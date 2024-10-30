using UserPortal.Models.Enums;

namespace UserPortal.Models.Dto
{
    public class CreateOrderDto
    {
        public string TokenName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public OrderType OrderType { get; set; }
    }
}
