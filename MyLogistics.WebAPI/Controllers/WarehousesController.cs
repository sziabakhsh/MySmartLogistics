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

        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseDto dto, CancellationToken ct)
        {
            var result = await _warehouseService.CreateWarehouseAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] CancellationToken ct)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id, ct);
            return warehouse is null ? NotFound() : Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetByTenant([FromQuery] CancellationToken ct)
        {
            var warehouses = await _warehouseService.GetWarehousesByTenantAsync(ct);
            return Ok(warehouses);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseDto dto, CancellationToken ct)
        {
            var updated = await _warehouseService.UpdateWarehouseAsync(id, dto, ct);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var deleted = await _warehouseService.DeleteWarehouseAsync(id, ct);
            return deleted ? NoContent() : NotFound();
        }
    }
}
