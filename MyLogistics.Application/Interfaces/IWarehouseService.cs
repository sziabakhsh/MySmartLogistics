using MyLogistics.Application.DTOs;

namespace MyLogistics.Application.Interfaces
{
    public interface IWarehouseService
    {
        Task<WarehouseDto> CreateWarehouseAsync(CreateWarehouseDto dto, CancellationToken ct = default);
        Task<WarehouseDto?> GetWarehouseByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<WarehouseDto>> GetWarehousesByTenantAsync(CancellationToken ct = default);
        Task<bool> UpdateWarehouseAsync(Guid id, UpdateWarehouseDto dto, CancellationToken ct = default);
        Task<bool> DeleteWarehouseAsync(Guid id, CancellationToken ct = default);
    }
}
