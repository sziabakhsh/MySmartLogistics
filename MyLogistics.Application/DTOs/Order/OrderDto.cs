using MyLogistics.Domain.Ordering.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Application.DTOs.Order
{
    // DTO for returning Order entity
    public record OrderDto (
        Guid Id,
        string TenantId,
        string OrderNumber,
        string Status,
        string ShippingAddress,
        decimal TotalAmount,
        string Currency,
        DateTime CreatedAtUtc
    );

    // DTO for creating a new Order
    public record CreateOrderDto(
        string TenantId,
        string Street,
        string City,
        string State,
        string ZipCode,
        string Country,
        decimal TotalAmount,
        string Currency
    );

    // DTO for updating order status
    public record UpdateOrderStatusDto(
        OrderStatus Status
    );

}
