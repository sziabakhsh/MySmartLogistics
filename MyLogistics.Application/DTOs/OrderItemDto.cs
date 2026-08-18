namespace MyLogistics.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string SKU { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice => (Quantity * UnitPrice) - DiscountAmount;
        public List<string> Attributes { get; set; } = new();
    }

    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public string SKU { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<string> Attributes { get; set; } = new();
    }

    public record UpdateOrderItemDto(
        string ProductName,
        int Quantity,
        decimal UnitPrice
    );

    public record DeleteOrderItemDto(
        Guid Id
    );
}
