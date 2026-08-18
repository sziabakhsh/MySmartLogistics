
using MyLogistics.Application.DTOs;

namespace MyLogistics.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, CancellationToken ct = default);
        Task<OrderDto?> GetOrderByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<OrderDto>> GetOrdersAsync(CancellationToken ct = default);
        Task<bool> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto dto, CancellationToken ct = default);
        Task<bool> DeleteOrderAsync(Guid id, CancellationToken ct = default);
    }
}
