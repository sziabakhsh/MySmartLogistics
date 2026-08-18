namespace MyLogistics.Application.DTOs
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; } = default!;
        public Guid WarehouseId { get; set; }
        public string Sku { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
    }

    public class CreateInventoryItemDto
    {
        public Guid WarehouseId { get; set; }
        public string Sku { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public record UpdateStockDto(
        int QuantityDelta // plus for increment, minus for decrement
    );
}
