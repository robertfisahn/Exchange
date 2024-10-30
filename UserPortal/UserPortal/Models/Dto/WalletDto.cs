namespace UserPortal.Models.Dto
{
    public class WalletDto
    {
        public List<WalletTokenDto>? Tokens { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
