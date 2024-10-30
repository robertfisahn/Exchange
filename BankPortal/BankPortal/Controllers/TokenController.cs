using BankPortal.Interface;
using BankPortal.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BankPortal.Controllers
{
    [ApiController]
    [Route("api/bank/token")]
    public class TokenController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        [HttpGet]
        public async Task<ActionResult<List<TokenDto>>> Get()
        {
            return await _tokenService.GetTokens();
        }
    }
}
