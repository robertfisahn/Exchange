using BankPortal.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankPortal.Controllers
{
    [ApiController]
    [Route("api/bank/wallet")]
    public class WalletController(IWalletService walletService) : ControllerBase
    {
        private readonly IWalletService _walletService = walletService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _walletService.GetWallets();
            return Ok();
        }

        [HttpGet("{userEmail}")]
        public async Task <IActionResult> GetByEmail(string userEmail)
        {
            var wallet = await _walletService.GetOrCreateWalletDto(userEmail);
            return Ok(wallet);
        }
    }
}
