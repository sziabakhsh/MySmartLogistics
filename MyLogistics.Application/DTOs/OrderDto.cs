using MyLogistics.Domain.Ordering.Entities;
using MyLogistics.Domain.Ordering.Enums;
using MyLogistics.Domain.Ordering.ValueObjects;

namespace MyLogistics.Application.DTOs
{
    // DTO for returning Order entity
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; } = default!;
        public string OrderNumber { get; set; } = default!;
        public string CustomerName { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ProcessedAtUtc { get; set; }
        public Address ShippingAddress { get; set; } = default!;
        public PaymentDetails Payment { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public List<OrderStatusLog> StatusHistory { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }

    // DTO for creating a new Order
    public class CreateOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public PriorityLevel Priority { get; set; } = PriorityLevel.Standard;
        public Address ShippingAddress { get; set; } = default!;
        public PaymentDetails Payment { get; set; } = default!;
        public List<string> Tags { get; set; } = new();
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }

    // DTO for updating order status
    public record UpdateOrderStatusDto(
        OrderStatus Status
    );

}
