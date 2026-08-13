using MyLogistics.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Application.Interfaces
{
    public interface IShipmentService
    {
        Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto dto, CancellationToken ct = default);
        Task<ShipmentDto?> GetShipmentByIdAsync(Guid id, string tenantId, CancellationToken ct = default);
        Task<IEnumerable<ShipmentDto>> GetShipmentsByTenantAsync(string tenantId, CancellationToken ct = default);
        Task<ShipmentDto?> GetShipmentByTrackingCodeAsync(string trackingCode, string tenantId, CancellationToken ct = default);
        Task<bool> UpdateShipmentStatusAsync(Guid id, string tenantId, UpdateShipmentStatusDto dto, CancellationToken ct = default);
        Task<bool> DeleteShipmentAsync(Guid id, string tenantId, CancellationToken ct = default);
    }
}
