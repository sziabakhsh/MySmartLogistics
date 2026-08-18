using MyLogistics.Application.DTOs;
using MyLogistics.Domain.Logistics.Entities;
using MyLogistics.Domain.Logistics.Enums;

namespace MyLogistics.Application.Mappers
{
    public static class ShipmentMapper
    {
        public static ShipmentDto ToDto(this Shipment shipment)
        {
            return new ShipmentDto
            {
                Id = shipment.Id,
                TenantId = shipment.TenantId,
                OrderId = shipment.OrderId,
                WarehouseId = shipment.WarehouseId,
                TrackingCode = shipment.TrackingCode,
                CarrierName = shipment.CarrierName,
                Status = shipment.Status,
                DispatchedAtUtc = shipment.DispatchedAtUtc,
                EstimatedDeliveryUtc = shipment.EstimatedDeliveryUtc,
                DeliveredAtUtc = shipment.DeliveredAtUtc
            };
        }

        public static Shipment ToEntity(this CreateShipmentDto dto, string tenantId)
        {
            return new Shipment
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                OrderId = dto.OrderId,
                WarehouseId = dto.WarehouseId,
                TrackingCode = dto.TrackingCode,
                CarrierName = dto.CarrierName,
                Status = ShipmentStatus.Created,
                DispatchedAtUtc = DateTime.UtcNow,
                EstimatedDeliveryUtc = null,
                DeliveredAtUtc = null
            };
        }
    }
}
