using MyLogistics.Application.DTOs;
using MyLogistics.Domain.Logistics.Entities;

namespace MyLogistics.Application.Mappers
{
    public static class ShipmentMapper
    {
        public static ShipmentDto ToDto(this Shipment shipment)
        {
            return new ShipmentDto(
                shipment.Id,
                shipment.TenantId,
                shipment.OrderId,
                shipment.WarehouseId,
                shipment.TrackingCode,
                shipment.CarrierName,
                shipment.Status.ToString(),
                shipment.DispatchedAtUtc,
                shipment.EstimatedDeliveryUtc,
                shipment.DeliveredAtUtc
            );
        }
    }
}
