using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.Interfaces;
using MyLogistics.Domain.Ordering.Entities;
using MyLogistics.Domain.Ordering.Enums;
using MyLogistics.Domain.Ordering.ValueObjects;
using MyLogistics.Application.Mappers;
using MyLogistics.Application.DTOs;

namespace MyLogistics.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAppDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public OrderService(IAppDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();
            var order = OrderMapper.ToEntity(dto, tenantId);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(ct);

            return OrderMapper.ToDto(order);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var order = await _context.Orders
                .Where(o => o.TenantId == tenantId)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            return order is null ? null : OrderMapper.ToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync(CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var orders = await _context.Orders
                .Where(o => o.TenantId == tenantId)
                .ToListAsync(ct);

            return orders.Select(OrderMapper.ToDto);
        }

        public async Task<bool> DeleteOrderAsync(Guid id, CancellationToken ct = default)
        {
            var tenantId = _tenantProvider.GetTenantId();

            var order = await _context.Orders
                .Where(o => o.TenantId == tenantId)
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            if (order is null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public Task<bool> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusDto dto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}