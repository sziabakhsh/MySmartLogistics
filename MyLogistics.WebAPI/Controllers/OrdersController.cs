using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLogistics.Application;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;

namespace MyLogistics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create a new order for a specific tenant. The order details are provided in the request body as a CreateOrderDto object. The method returns the created order with its unique identifier and tenant information.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto, CancellationToken ct)
        {
            var result = await _orderService.CreateOrderAsync(dto, ct);
            return CreatedAtAction(nameof(GetOrderById), new { id = result.Id, tenantId = result.TenantId }, result);
        }

        /// <summary>
        /// Get the details of a specific order by its ID and tenant ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id, [FromQuery] string tenantId, CancellationToken ct)
        {
            var order = await _orderService.GetOrderByIdAsync(id, tenantId, ct);
            return order is null ? NotFound() : Ok(order);
        }

        /// <summary>
        /// Get all orders for a specific tenant.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOrdersByTenant([FromQuery] string tenantId, CancellationToken ct)
        {
            var orders = await _orderService.GetOrdersByTenantAsync(tenantId, ct);
            return Ok(orders);
        }

        /// <summary>
        /// Update the status of a specific order by its ID and tenant ID. The new status is provided in the request body as an UpdateOrderStatusDto object. The method returns NoContent if the update is successful, or NotFound if the order does not exist.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <param name="dto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromQuery] string tenantId, [FromBody] UpdateOrderStatusDto dto, CancellationToken ct)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(id, tenantId, dto, ct);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete a specific order by its ID and tenant ID. The method returns NoContent if the deletion is successful, or NotFound if the order does not exist.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id, [FromQuery] string tenantId, CancellationToken ct)
        {
            var deleted = await _orderService.DeleteOrderAsync(id, tenantId, ct);
            return deleted ? NoContent() : NotFound();
        }
    }
}
