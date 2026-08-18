using MyLogistics.Application.DTOs;
using MyLogistics.Domain.Logistics.Entities;

namespace MyLogistics.Application.Mappers
{
    public static class WarehouseMapper
    {
        public static WarehouseDto ToDto(this Warehouse warehouse)
        {
            return new WarehouseDto
            {
                Id = warehouse.Id,
                TenantId = warehouse.TenantId,
                Name = warehouse.Name,
                Code = warehouse.Code,
                LocationName = warehouse.LocationName,
                Capacity = warehouse.Capacity,
                IsActive = warehouse.IsActive,
                CreatedAtUtc = warehouse.CreatedAtUtc
            };
        }

        public static Warehouse ToEntity(this CreateWarehouseDto dto, string tenantId)
        {
            return new Warehouse
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = dto.Name,
                Code = dto.Code,
                LocationName = dto.LocationName,
                Capacity = dto.Capacity,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }

}
