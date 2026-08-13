using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Application.DTOs
{
    public record ShipmentDto(
        Guid Id,
        string TenantId,
        Guid OrderId,
        Guid WarehouseId,
        string TrackingCode,
        string CarrierName,
        string Status,
        DateTime DispatchedAtUtc,
        DateTime? EstimatedDeliveryUtc,
        DateTime? DeliveredAtUtc
    );

    public record CreateShipmentDto(
        string TenantId,
        Guid OrderId,
        Guid WarehouseId,
        string TrackingCode,
        string CarrierName
    );

    public record UpdateShipmentStatusDto(
        string Status,
        DateTime? EstimatedDeliveryUtc
    );


}
