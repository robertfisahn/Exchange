using BankPortal.Interface;
using BankPortal.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BankPortal.Controllers
{
    [ApiController]
    [Route("api/bank/order")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> NewOrder([FromBody] OrderDto order)
        {
            await _orderService.ProcessOrder(order);
            return Ok();
        }
    }
}
