using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;
using MyLogistics.Application.Mappers;
using MyLogistics.Domain.Logistics.Entities;

namespace MyLogistics.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IAppDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public WarehouseService(IAppDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto dto, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouse = dto.ToEntity(tenantId);

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync(ct);

            return warehouse.ToDto();
        }

        public async Task<WarehouseDto?> GetWarehouseByIdAsync(Guid id, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouse = await _context.Warehouses
                .AsNoTracking()
                .Where(w => w.TenantId == tenantId)
                .FirstOrDefaultAsync(w => w.Id == id, ct);

            return warehouse is null ? null : warehouse.ToDto();
        }

        public async Task<IEnumerable<WarehouseDto>> GetWarehousesByTenantAsync(CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouses = await _context.Warehouses
                .AsNoTracking()
                .Where(w => w.TenantId == tenantId)
                .ToListAsync(ct);

            return warehouses.Select(w => w.ToDto());
        }

        public async Task<bool> UpdateWarehouseAsync(Guid id, UpdateWarehouseDto dto, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouse = await _context.Warehouses
                .Where(w => w.TenantId == tenantId)
                .FirstOrDefaultAsync(w => w.Id == id, ct);

            if (warehouse is null) return false;

            warehouse.Name = dto.Name;
            warehouse.LocationName = dto.LocationName;
            warehouse.Capacity = dto.Capacity;
            warehouse.IsActive = dto.IsActive;

            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteWarehouseAsync(Guid id, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var warehouse = await _context.Warehouses
                .Where(w => w.TenantId == tenantId)
                .FirstOrDefaultAsync(w => w.Id == id, ct);

            if (warehouse is null) return false;

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
