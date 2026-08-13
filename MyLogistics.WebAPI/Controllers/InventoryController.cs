using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;

namespace MyLogistics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryItemDto dto, CancellationToken ct)
        {
            var result = await _inventoryService.CreateInventoryItemAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id, tenantId = result.TenantId }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] string tenantId, CancellationToken ct)
        {
            var item = await _inventoryService.GetInventoryItemByIdAsync(id, tenantId, ct);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpGet("warehouse/{warehouseId:guid}")]
        public async Task<IActionResult> GetByWarehouse(Guid warehouseId, [FromQuery] string tenantId, CancellationToken ct)
        {
            var items = await _inventoryService.GetInventoryByWarehouseAsync(warehouseId, tenantId, ct);
            return Ok(items);
        }

        [HttpPatch("{id:guid}/stock")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromQuery] string tenantId, [FromBody] UpdateStockDto dto, CancellationToken ct)
        {
            var updated = await _inventoryService.UpdateStockAsync(id, tenantId, dto, ct);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] string tenantId, CancellationToken ct)
        {
            var deleted = await _inventoryService.DeleteInventoryItemAsync(id, tenantId, ct);
            return deleted ? NoContent() : NotFound();
        }
    }
}
