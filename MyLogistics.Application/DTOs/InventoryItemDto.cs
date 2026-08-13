namespace MyLogistics.Application.DTOs
{
    public record InventoryItemDto(
        Guid Id,
        string TenantId,
        Guid WarehouseId,
        string Sku,
        string ProductName,
        int Quantity,
        int ReorderLevel,
        decimal UnitPrice,
        DateTime LastUpdatedUtc
    );

    public record CreateInventoryItemDto(
        string TenantId,
        Guid WarehouseId,
        string Sku,
        string ProductName,
        int Quantity,
        int ReorderLevel,
        decimal UnitPrice
    );

    public record UpdateStockDto(
        int QuantityDelta // plus for increment, minus for decrement
    );
}
