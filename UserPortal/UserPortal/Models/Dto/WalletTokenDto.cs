namespace UserPortal.Models.Dto
{
    public class WalletTokenDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Quantity { get; set; }
        public decimal TokenBalance { get; set; }
    }
}
