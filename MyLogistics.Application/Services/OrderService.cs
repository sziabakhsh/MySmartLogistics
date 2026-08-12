using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.DTOs.Order;
using MyLogistics.Application.Interfaces;
using MyLogistics.Domain.Ordering.Entities;
using MyLogistics.Domain.Ordering.Enums;
using MyLogistics.Domain.Ordering.ValueObjects;
using MyLogistics.Application.Mappers;

namespace MyLogistics.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAppDbContext _context;

        public OrderService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, CancellationToken ct = default)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                TenantId = dto.TenantId,
                OrderNumber = $"ORD-{DateTime.UtcNow.Ticks.ToString()[^6..]}",
                CreatedAtUtc = DateTime.UtcNow,
                Status = OrderStatus.Pending,

                ShippingAddress = new Address
                {
                    Street = dto.Street,
                    City = dto.City,
                    StateOrProvince = dto.State,
                    PostalCode = dto.ZipCode,
                    Country = dto.Country
                }
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(ct);

            //return MapToDto(order);
            return order.ToDto();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Where(o => o.TenantId == tenantId)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            //return order is null ? null : MapToDto(order);

            return order is null ? null : order.ToDto();
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByTenantAsync(string tenantId, CancellationToken ct = default)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.TenantId == tenantId)
                .ToListAsync(ct);

            //return orders.Select(MapToDto);
            return orders.Select(order => order.ToDto());
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid id, string tenantId, UpdateOrderStatusDto dto, CancellationToken ct = default)
        {
            var order = await _context.Orders
                .Where(o => o.TenantId == tenantId)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            if (order is null) return false;

            // Safe parsing from DTO string to Enum
            //if (Enum.TryParse<OrderStatus>(dto.Status, ignoreCase: true, out var newStatus))
            //{
                order.Status = dto.Status;
                await _context.SaveChangesAsync(ct);
                return true;
            //}

            //return false;
        }

        public async Task<bool> DeleteOrderAsync(Guid id, string tenantId, CancellationToken ct = default)
        {
            var order = await _context.Orders
                .Where(o => o.TenantId == tenantId)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            if (order is null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        //private static OrderDto MapToDto(Order order) => new(
        //    order.Id,
        //    order.TenantId,
        //    order.OrderNumber,
        //    order.Status.ToString(),
        //    $"{order.ShippingAddress?.Street}, {order.ShippingAddress?.City}",
        //    order.TotalAmount?.Amount ?? 0,
        //    order.TotalAmount?.Currency ?? "CAD",
        //    order.CreatedAtUtc
        //);
    }
}