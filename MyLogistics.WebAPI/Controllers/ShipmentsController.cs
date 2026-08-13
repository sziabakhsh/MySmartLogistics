using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;

namespace MyLogistics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentsController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShipmentDto dto, CancellationToken ct)
        {
            var result = await _shipmentService.CreateShipmentAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id, tenantId = result.TenantId }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] string tenantId, CancellationToken ct)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id, tenantId, ct);
            return shipment is null ? NotFound() : Ok(shipment);
        }

        [HttpGet]
        public async Task<IActionResult> GetByTenant([FromQuery] string tenantId, CancellationToken ct)
        {
            var shipments = await _shipmentService.GetShipmentsByTenantAsync(tenantId, ct);
            return Ok(shipments);
        }

        [HttpGet("tracking/{trackingNumber}")]
        public async Task<IActionResult> GetByTrackingNumber(string trackingNumber, [FromQuery] string tenantId, CancellationToken ct)
        {
            var shipment = await _shipmentService.GetShipmentByTrackingCodeAsync(trackingNumber, tenantId, ct);
            return shipment is null ? NotFound() : Ok(shipment);
        }

        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] string tenantId, [FromBody] UpdateShipmentStatusDto dto, CancellationToken ct)
        {
            var updated = await _shipmentService.UpdateShipmentStatusAsync(id, tenantId, dto, ct);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] string tenantId, CancellationToken ct)
        {
            var deleted = await _shipmentService.DeleteShipmentAsync(id, tenantId, ct);
            return deleted ? NoContent() : NotFound();
        }
    }
}
