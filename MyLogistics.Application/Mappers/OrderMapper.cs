using MyLogistics.Application.DTOs.Order;
using MyLogistics.Domain.Ordering.Entities;
using MyLogistics.Domain.Tenancy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Application.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto(
                order.Id,
                order.TenantId,
                order.OrderNumber,
                order.Status.ToString(),
                $"{order.ShippingAddress?.Street}, {order.ShippingAddress?.City}",
                order.TotalAmount?.Amount ?? 0,
                order.TotalAmount?.Currency ?? "CAD",
                order.CreatedAtUtc
            );
        }
    }
}
