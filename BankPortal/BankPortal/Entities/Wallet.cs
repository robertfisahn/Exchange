using BankPortal.Models.Dto;

namespace BankPortal.Entities
{
    public class Wallet
    {
        public string Id { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public virtual List<WalletTokenDto>? Tokens { get; set; }
    }
}
