using MyLogistics.Domain.Ordering.ValueObjects;
using MyLogistics.Domain.Ordering.Enums;

namespace MyLogistics.Domain.Ordering.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; } = default!; // Cosmos DB Partition Key
        public string OrderNumber { get; set; } = default!;
        public string CustomerName { get; set; }=string.Empty;

        public OrderStatus Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ProcessedAtUtc { get; set; }

        // Value Objects / Embedded Documents
        public Address ShippingAddress { get; set; } = default!;
        public PaymentDetails Payment { get; set; } = default!;
        public Money TotalAmount { get; set; } = default!;

        // Embedded Collections (Nested Objects in NoSQL)
        public List<OrderItem> Items { get; set; } = new();
        public List<OrderStatusLog> StatusHistory { get; set; } = new();
        public List<string> Tags { get; set; } = new(); // e.g., "Express", "Fragile", "VIP"
    }
}
