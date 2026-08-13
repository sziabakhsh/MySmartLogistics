using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.DTOs;
using MyLogistics.Application.Interfaces;
using MyLogistics.Domain.Logistics.Entities;

namespace MyLogistics.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IAppDbContext _context;

        public WarehouseService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto dto, CancellationToken ct = default)
        {
            var warehouse = new Warehouse
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId,
                Name = dto.Name,
                Code = dto.Code,
                LocationName = dto.LocationName,
                Capacity = dto.Capacity,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync(ct);

            return MapToDto(warehouse);
        }

        public async Task<WarehouseDto?> GetWarehouseByIdAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var warehouse = await _context.Warehouses
                .AsNoTracking()
                .Where(w => w.TenantId == tenantId)
                .FirstOrDefaultAsync(w => w.Id == id, ct);

            return warehouse is null ? null : MapToDto(warehouse);
        }

        public async Task<IEnumerable<WarehouseDto>> GetWarehousesByTenantAsync(string tenantId, CancellationToken ct = default)
        {
            var warehouses = await _context.Warehouses
                .AsNoTracking()
                .Where(w => w.TenantId == tenantId)
                .ToListAsync(ct);

            return warehouses.Select(MapToDto);
        }

        public async Task<bool> UpdateWarehouseAsync(Guid id, string tenantId, UpdateWarehouseDto dto, CancellationToken ct = default)
        {
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

        public async Task<bool> DeleteWarehouseAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var warehouse = await _context.Warehouses
                .Where(w => w.TenantId == tenantId)
                .FirstOrDefaultAsync(w => w.Id == id, ct);

            if (warehouse is null) return false;

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        private static WarehouseDto MapToDto(Warehouse w) => new(
            w.Id,
            w.TenantId,
            w.Name,
            w.Code,
            w.LocationName,
            w.Capacity,
            w.IsActive,
            w.CreatedAtUtc
        );
    }
}
