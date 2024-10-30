namespace TokenPortal.Dto.Models
{
    public class TokenDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal MarketCap => Price * TotalSupply;
    }
}   
