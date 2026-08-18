using MyLogistics.Domain.Logistics.Entities;
using MyLogistics.Application.DTOs;

namespace MyLogistics.Application.Mappers
{
    public static class InventoryItemMapper
    {
        public static InventoryItemDto ToDto(this InventoryItem inventoryItem)
        {
            return new InventoryItemDto
            {
                Id = inventoryItem.Id,
                TenantId = inventoryItem.TenantId,
                WarehouseId = inventoryItem.WarehouseId,
                Sku = inventoryItem.Sku,
                ProductName = inventoryItem.ProductName,
                Quantity = inventoryItem.Quantity,
                ReorderLevel = inventoryItem.ReorderLevel,
                UnitPrice = inventoryItem.UnitPrice,
                LastUpdatedUtc = inventoryItem.LastUpdatedUtc
            };
        }

        public static InventoryItem ToEntity(this CreateInventoryItemDto dto, string tenantId)
        {
            return new InventoryItem
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                WarehouseId = dto.WarehouseId,
                Sku = dto.Sku,
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                ReorderLevel = dto.ReorderLevel,
                UnitPrice = dto.UnitPrice,
                LastUpdatedUtc = DateTime.UtcNow
            };
        }
    }
}
