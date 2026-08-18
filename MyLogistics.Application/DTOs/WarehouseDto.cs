
namespace MyLogistics.Application.DTOs
{
    public record WarehouseDto(
        Guid Id,
        string TenantId,
        string Name,
        string Code,
        string LocationName,
        int Capacity,
        bool IsActive,
        DateTime CreatedAtUtc
    );

    public record CreateWarehouseDto(
        string Name,
        string Code,
        string LocationName,
        int Capacity
    );

    public record UpdateWarehouseDto(
        string Name,
        string LocationName,
        int Capacity,
        bool IsActive
    );

}
