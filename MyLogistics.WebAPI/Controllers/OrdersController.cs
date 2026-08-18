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

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto, CancellationToken ct)
        {
            var result = await _orderService.CreateOrderAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _orderService.GetOrderByIdAsync(id, ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll(CancellationToken ct)
        {
            var result = await _orderService.GetOrdersAsync(ct);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var success = await _orderService.DeleteOrderAsync(id, ct);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
