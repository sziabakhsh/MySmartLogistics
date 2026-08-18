using MyLogistics.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryItemDto> CreateInventoryItemAsync(CreateInventoryItemDto dto, CancellationToken ct = default);
        Task<InventoryItemDto?> GetInventoryItemByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<InventoryItemDto>> GetInventoryByWarehouseAsync(Guid warehouseId, CancellationToken ct = default);
        Task<bool> UpdateStockAsync(Guid id, UpdateStockDto dto, CancellationToken ct = default);
        Task<bool> DeleteInventoryItemAsync(Guid id, CancellationToken ct = default);
    }
}
