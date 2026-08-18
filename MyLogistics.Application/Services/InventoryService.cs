using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;
using MyLogistics.Application.Mappers;
using MyLogistics.Domain.Logistics.Entities;


namespace MyLogistics.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IAppDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public InventoryService(IAppDbContext context, ITenantProvider tenantProvider   )
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto dto, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var item = dto.ToEntity(tenantId);

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync(ct);

            return item.ToDto();
        }

        public async Task<InventoryItemDto?> GetInventoryItemByIdAsync(Guid id, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var item = await _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.TenantId == tenantId)
                .FirstOrDefaultAsync(i => i.Id == id, ct);

            return item is null ? null : item.ToDto();
        }

        public async Task<IEnumerable<InventoryItemDto>> GetInventoryByWarehouseAsync(Guid warehouseId, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var items = await _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.TenantId == tenantId && i.WarehouseId == warehouseId)
                .ToListAsync(ct);

            return items.Select(item => item.ToDto());
        }

        public async Task<bool> UpdateStockAsync(Guid id, UpdateStockDto dto, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var item = await _context.InventoryItems
                .Where(i => i.TenantId == tenantId)
                .FirstOrDefaultAsync(i => i.Id == id, ct);

            if (item is null) return false;

            item.Quantity += dto.QuantityDelta;
            item.LastUpdatedUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteInventoryItemAsync(Guid id, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var item = await _context.InventoryItems
                .Where(i => i.TenantId == tenantId)
                .FirstOrDefaultAsync(i => i.Id == id, ct);

            if (item is null) return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync(ct);
            return true;
        }

    }
}
