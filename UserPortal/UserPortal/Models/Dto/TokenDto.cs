namespace UserPortal.Models.Dto
{
    public class TokenDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal MarketCap { get; set; }
    }
}
