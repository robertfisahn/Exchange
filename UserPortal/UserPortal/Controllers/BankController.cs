using Microsoft.AspNetCore.Mvc;
using UserPortal.Interfaces;
using UserPortal.Models.Dto;

namespace UserPortal.Controllers
{
    [ApiController]
    [Route("api/bank")]
    public class BankController(IBankService tokenService) : ControllerBase
    {
        private readonly IBankService _bankService = tokenService;

        [HttpGet("tokens")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<TokenDto>>> Get()
        {
            var tokens = await _bankService.GetTokensAsync();
            return Ok(tokens);
        }

        [HttpGet("wallet")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<WalletDto>> GetWallet()
        {
            var wallet = await _bankService.GetWallet();
            return Ok(wallet);
        }

        [HttpPost("order")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> NewOrder([FromBody] CreateOrderDto order)
        {
            await _bankService.CreateOrder(order);

            return Ok();
        }
    }
}
