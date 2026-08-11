using MyLogistics.Domain.Ordering.Enums;

namespace MyLogistics.Domain.Ordering.Entities
{
    // history of status changes for analytical queries (time-bucketed)
    public class OrderStatusLog
    {
        public OrderStatus Status { get; set; }
        public DateTime TimestampUtc { get; set; }
        public string? Reason { get; set; }
        public string ChangedByUserId { get; set; } = default!;
    }
}
