using MyLogistics.Domain.Common.ValueObjects;
using MyLogistics.Domain.Logistics.Enums;

namespace MyLogistics.Domain.Logistics.Entities
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; } = default!; // Partition Key
        public Guid OrderId { get; set; }
        public string TrackingCode { get; set; } = default!;
        public Guid WarehouseId { get; set; }
        public string CarrierName { get; set; } = default!;

        public ShipmentStatus Status { get; set; }
        public DateTime DispatchedAtUtc { get; set; }
        public DateTime? EstimatedDeliveryUtc { get; set; }
        public DateTime? DeliveredAtUtc { get; set; }

        // Geo-Location data for routing queries
        public Location CurrentLocation { get; set; } = default!;
        public List<Checkpoint> RouteCheckpoints { get; set; } = new();
    }

    public class Checkpoint
    {
        public string LocationName { get; set; } = default!;
        public DateTime ArrivalTimeUtc { get; set; }
        public string StatusNote { get; set; } = default!;
    }
}
