using MyLogistics.Application.DTOs;
using MyLogistics.Domain.Ordering.Entities;
using MyLogistics.Domain.Ordering.Enums;
using MyLogistics.Domain.Ordering.ValueObjects;
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
        public static OrderDto ToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                TenantId = order.TenantId,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                Status = order.Status,
                Priority = order.Priority,
                CreatedAtUtc = order.CreatedAtUtc,
                ProcessedAtUtc = order.ProcessedAtUtc,
                ShippingAddress = order.ShippingAddress,
                Payment = order.Payment,
                TotalAmount = order.TotalAmount?.Amount ?? 0m,
                StatusHistory = order.StatusHistory ?? new(),
                Tags = order.Tags ?? new(),
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    SKU = i.SKU,
                    ProductName = i.ProductName,
                    Category = i.Category,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    DiscountAmount = i.DiscountAmount,
                    Attributes = i.Attributes ?? new()
                }).ToList()
            };
        }

        public static Order ToEntity(CreateOrderDto dto, string tenantId)
        {
            var items = dto.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                SKU = i.SKU,
                ProductName = i.ProductName,
                Category = i.Category,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                DiscountAmount = i.DiscountAmount,
                Attributes = i.Attributes ?? new()
            }).ToList();

            // محاسبه مجموع مبلغ سفارش
            var totalAmountValue = items.Sum(i => (i.Quantity * i.UnitPrice) - i.DiscountAmount);

            return new Order
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}",
                CustomerName = dto.CustomerName,
                Status = OrderStatus.Pending,
                Priority = dto.Priority,
                CreatedAtUtc = DateTime.UtcNow,
                ShippingAddress = dto.ShippingAddress,
                Payment = dto.Payment,
                TotalAmount = new Money(totalAmountValue, "CAD"),
                Items = items,
                Tags = dto.Tags ?? new(),
                StatusHistory = new List<OrderStatusLog>
            {
                new OrderStatusLog
                {
                    Status = OrderStatus.Pending,
                    TimestampUtc = DateTime.UtcNow,
                    Reason = "Order created successfully."
                }
            }
            };
        }
    }
}
