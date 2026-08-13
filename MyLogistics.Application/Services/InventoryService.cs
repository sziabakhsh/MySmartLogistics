using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;
using MyLogistics.Domain.Logistics.Entities;


namespace MyLogistics.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IAppDbContext _context;

        public InventoryService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto dto, CancellationToken ct = default)
        {
            var item = new InventoryItem
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId,
                WarehouseId = dto.WarehouseId,
                Sku = dto.Sku,
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                ReorderLevel = dto.ReorderLevel,
                UnitPrice = dto.UnitPrice,
                LastUpdatedUtc = DateTime.UtcNow
            };

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync(ct);

            return MapToDto(item);
        }

        public async Task<InventoryItemDto?> GetInventoryItemByIdAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var item = await _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.TenantId == tenantId)
                .FirstOrDefaultAsync(i => i.Id == id, ct);

            return item is null ? null : MapToDto(item);
        }

        public async Task<IEnumerable<InventoryItemDto>> GetInventoryByWarehouseAsync(Guid warehouseId, string tenantId, CancellationToken ct = default)
        {
            var items = await _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.TenantId == tenantId && i.WarehouseId == warehouseId)
                .ToListAsync(ct);

            return items.Select(MapToDto);
        }

        public async Task<bool> UpdateStockAsync(Guid id, string tenantId, UpdateStockDto dto, CancellationToken ct = default)
        {
            var item = await _context.InventoryItems
                .Where(i => i.TenantId == tenantId)
                .FirstOrDefaultAsync(i => i.Id == id, ct);

            if (item is null) return false;

            item.Quantity += dto.QuantityDelta;
            item.LastUpdatedUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteInventoryItemAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var item = await _context.InventoryItems
                .Where(i => i.TenantId == tenantId)
                .FirstOrDefaultAsync(i => i.Id == id, ct);

            if (item is null) return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        private static InventoryItemDto MapToDto(InventoryItem i) => new(
            i.Id,
            i.TenantId,
            i.WarehouseId,
            i.Sku,
            i.ProductName,
            i.Quantity,
            i.ReorderLevel,
            i.UnitPrice,
            i.LastUpdatedUtc
        );
    }
}
