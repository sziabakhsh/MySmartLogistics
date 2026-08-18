using MyLogistics.Domain.Common.ValueObjects;
using MyLogistics.Domain.Logistics.Enums;

namespace MyLogistics.Application.DTOs
{
    public class ShipmentDto
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; } = default!;
        public Guid OrderId { get; set; }
        public string TrackingCode { get; set; } = default!;
        public Guid WarehouseId { get; set; }
        public string CarrierName { get; set; } = default!;
        public ShipmentStatus Status { get; set; }
        public DateTime DispatchedAtUtc { get; set; }
        public DateTime? EstimatedDeliveryUtc { get; set; }
        public DateTime? DeliveredAtUtc { get; set; }
        public Location CurrentLocation { get; set; } = default!;
        public List<CheckpointDto> RouteCheckpoints { get; set; } = new();
    }

    public class CheckpointDto
    {
        public string LocationName { get; set; } = default!;
        public DateTime ArrivalTimeUtc { get; set; }
        public string StatusNote { get; set; } = default!;
    }

    public class CreateShipmentDto
    {
        public Guid OrderId { get; set; }
        public Guid WarehouseId { get; set; }
        public string CarrierName { get; set; } = default!;
        public DateTime? EstimatedDeliveryUtc { get; set; }
        public Location CurrentLocation { get; set; } = default!;
        public string TrackingCode { get; set; } = default!;
    }

    public class CreateCheckpointDto
    {
        public string LocationName { get; set; } = default!;
        public string StatusNote { get; set; } = default!;
    }
}
