namespace BankPortal.Models.Dto
{
    public class WalletDto
    {
        public List<WalletTokenReadDto>? Tokens { get; set; }
        public decimal TotalBalance => Tokens?.Sum(t => t.TokenBalance) ?? 0;
    }
}
