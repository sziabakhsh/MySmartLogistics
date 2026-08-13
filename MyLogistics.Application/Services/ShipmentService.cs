using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;
using MyLogistics.Domain.Logistics.Entities;
using MyLogistics.Domain.Logistics.Enums;
using MyLogistics.Application.Mappers;
using Microsoft.EntityFrameworkCore;

namespace MyLogistics.Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IAppDbContext _context;

        public ShipmentService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto dto, CancellationToken ct = default)
        {
            var shipment = new Shipment
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId,
                OrderId = dto.OrderId,
                WarehouseId = dto.WarehouseId,
                TrackingCode = dto.TrackingCode,
                CarrierName = dto.CarrierName,
                Status = ShipmentStatus.Created,
                DispatchedAtUtc = DateTime.UtcNow
            };

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync(ct);

            //return MapToDto(shipment);
            return shipment.ToDto();
        }

        public async Task<ShipmentDto?> GetShipmentByIdAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var shipment = await _context.Shipments
                .AsNoTracking()
                .Where(s => s.TenantId == tenantId)
                .FirstOrDefaultAsync(s => s.Id == id, ct);

            //return shipment is null ? null : MapToDto(shipment);

            return shipment is null ? null : shipment.ToDto();
        }

        public async Task<IEnumerable<ShipmentDto>> GetShipmentsByTenantAsync(string tenantId, CancellationToken ct = default)
        {
            var shipments = await _context.Shipments
                .AsNoTracking()
                .Where(s => s.TenantId == tenantId)
                .ToListAsync(ct);

            //return shipments.Select(MapToDto);
            return shipments.Select(o=>o.ToDto());
        }

        public async Task<ShipmentDto?> GetShipmentByTrackingCodeAsync(string trackingCode, string tenantId, CancellationToken ct = default)
        {
            var shipment = await _context.Shipments
                .AsNoTracking()
                .Where(s => s.TenantId == tenantId)
                .FirstOrDefaultAsync(s => s.TrackingCode == trackingCode, ct);

            //return shipment is null ? null : MapToDto(shipment);
            return shipment is null ? null : shipment.ToDto();
        }

        public async Task<bool> UpdateShipmentStatusAsync(Guid id, string tenantId, UpdateShipmentStatusDto dto, CancellationToken ct = default)
        {
            var shipment = await _context.Shipments
                .Where(s => s.TenantId == tenantId)
                .FirstOrDefaultAsync(s => s.Id == id, ct);

            if (shipment is null) return false;

            if (Enum.TryParse<ShipmentStatus>(dto.Status, ignoreCase: true, out var newStatus))
            {
                shipment.Status = newStatus;

                if (newStatus == ShipmentStatus.Delivered)
                {
                    shipment.DeliveredAtUtc = DateTime.UtcNow;
                }

                if (dto.EstimatedDeliveryUtc.HasValue)
                {
                    shipment.EstimatedDeliveryUtc = dto.EstimatedDeliveryUtc;
                }

                await _context.SaveChangesAsync(ct);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteShipmentAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var shipment = await _context.Shipments
                .Where(s => s.TenantId == tenantId)
                .FirstOrDefaultAsync(s => s.Id == id, ct);

            if (shipment is null) return false;

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        //private static ShipmentDto MapToDto(Shipment shipment) => new(
        //    shipment.Id,
        //    shipment.TenantId,
        //    shipment.OrderId,
        //    shipment.WarehouseId,
        //    shipment.TrackingCode,
        //    shipment.CarrierName,
        //    shipment.Status.ToString(),
        //    shipment.DispatchedAtUtc,
        //    shipment.EstimatedDeliveryUtc,
        //    shipment.DeliveredAtUtc
        //);
    }
}

