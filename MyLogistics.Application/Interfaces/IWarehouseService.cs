using MyLogistics.Application.DTOs;

namespace MyLogistics.Application.Interfaces
{
    public interface IWarehouseService
    {
        Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto dto, CancellationToken ct = default);
        Task<WarehouseDto?> GetWarehouseByIdAsync(Guid id, string tenantId, CancellationToken ct = default);
        Task<IEnumerable<WarehouseDto>> GetWarehousesByTenantAsync(string tenantId, CancellationToken ct = default);
        Task<bool> UpdateWarehouseAsync(Guid id, string tenantId, UpdateWarehouseDto dto, CancellationToken ct = default);
        Task<bool> DeleteWarehouseAsync(Guid id, string tenantId, CancellationToken ct = default);
    }
}
