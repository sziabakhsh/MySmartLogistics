
namespace MyLogistics.Application.DTOs
{
    public class WarehouseDto
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string LocationName { get; set; } = default!;
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public List<InventoryItemDto> InventoryItems { get; set; } = new();
    }

    public class CreateWarehouseDto
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!; // e.g. "WH-YVR-01"
        public string LocationName { get; set; } = default!;
        public int Capacity { get; set; }
    }

       public record UpdateWarehouseDto(
        string Name,
        string LocationName,
        int Capacity,
        bool IsActive
    );

}
