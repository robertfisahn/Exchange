namespace TokenPortal.Models.Dto
{
    public class TokenCreateDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal TotalSupply { get; set; }
    }
}
