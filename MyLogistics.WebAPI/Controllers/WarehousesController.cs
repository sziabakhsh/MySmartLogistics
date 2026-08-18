using Microsoft.AspNetCore.Mvc;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;

namespace MyLogistics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly ITenantProvider _tenantProvider;

        public WarehousesController(IWarehouseService warehouseService, ITenantProvider tenantProvider)
        {
            _warehouseService = warehouseService;
            _tenantProvider = tenantProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseDto dto, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var result = await _warehouseService.CreateWarehouseAsync(dto, tenantId, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id, tenantId = result.TenantId }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id, tenantId, ct);
            return warehouse is null ? NotFound() : Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetByTenant([FromQuery] CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouses = await _warehouseService.GetWarehousesByTenantAsync(tenantId, ct);
            return Ok(warehouses);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseDto dto, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var updated = await _warehouseService.UpdateWarehouseAsync(id, tenantId, dto, ct);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var deleted = await _warehouseService.DeleteWarehouseAsync(id, tenantId, ct);
            return deleted ? NoContent() : NotFound();
        }
    }
}
