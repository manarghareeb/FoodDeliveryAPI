using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest orderRequest) 
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.OrderService.CreateOrderAsync(orderRequest, userEmail));
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id) 
            => Ok(await _serviceManager.OrderService.GetOrderByIdAsync(id));
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrdersByEmailAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.OrderService.GetOrdersByEmailAsync(userEmail));
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
            => Ok(await _serviceManager.OrderService.GetDeliveryMethodsAsync());
    }
}
