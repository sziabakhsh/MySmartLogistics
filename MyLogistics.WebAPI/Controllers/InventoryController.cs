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
        private readonly ITenantProvider _tenantProvider;
        public InventoryController(IInventoryService inventoryService, ITenantProvider tenantProvider)
        {
            _inventoryService = inventoryService;
            _tenantProvider = tenantProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryItemDto dto, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var result = await _inventoryService.CreateInventoryItemAsync(dto, tenantId, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id, tenantId = result.TenantId }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var item = await _inventoryService.GetInventoryItemByIdAsync(id, tenantId, ct);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpGet("warehouse/{warehouseId:guid}")]
        public async Task<IActionResult> GetByWarehouse(Guid warehouseId, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var items = await _inventoryService.GetInventoryByWarehouseAsync(warehouseId, tenantId, ct);
            return Ok(items);
        }

        [HttpPatch("{id:guid}/stock")]
        public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockDto dto, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var updated = await _inventoryService.UpdateStockAsync(id, tenantId, dto, ct);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var deleted = await _inventoryService.DeleteInventoryItemAsync(id, tenantId, ct);
            return deleted ? NoContent() : NotFound();
        }
    }
}
