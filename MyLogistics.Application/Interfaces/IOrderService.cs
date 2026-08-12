
using MyLogistics.Application.DTOs.Order;

namespace MyLogistics.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, CancellationToken ct = default);
        Task<OrderDto?> GetOrderByIdAsync(Guid id, string tenantId, CancellationToken ct = default);
        Task<IEnumerable<OrderDto>> GetOrdersByTenantAsync(string tenantId, CancellationToken ct = default);
        Task<bool> UpdateOrderStatusAsync(Guid id, string tenantId, UpdateOrderStatusDto dto, CancellationToken ct = default);
        Task<bool> DeleteOrderAsync(Guid id, string tenantId, CancellationToken ct = default);
    }
}
