using Microsoft.AspNetCore.Mvc;
using TokenPortal.Dto.Models;
using TokenPortal.Entities;
using TokenPortal.Interface;
using TokenPortal.Models.Dto;

namespace TokenPortal.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        [HttpGet]
        public async Task<ActionResult<List<TokenDto>>> Get()
        {
            var tokens = await _tokenService.GetTokens();
            return Ok(tokens);
        }

        [HttpGet("{tokenName}")]
        public async Task<ActionResult<TokenDto>> GetByName(string tokenName)
        {
            var token = await _tokenService.GetTokenByName(tokenName);
            return Ok(token);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateToken([FromBody] Token token)
        {
            await _tokenService.UpdateToken(token);
            return NoContent();
        }

        [HttpPut("update-price")]
        public async Task<ActionResult> UpdateTokenPrice([FromBody] TokenUpdateDto dto)
        {
            await _tokenService.Update(dto);
            return NoContent();
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<List<TokenDto>>> GetWalletTokens([FromBody] List<string> tokens)
        {
            var tokenList = await _tokenService.GetWalletTokens(tokens);
            return Ok(tokenList);
        }
    }
}
