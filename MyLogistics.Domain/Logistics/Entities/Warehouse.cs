using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Logistics.Entities
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        // Partiotion key for Cosmos DB and Multi-Tenancy
        public string TenantId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!; // e.g. "WH-YVR-01"
        public string LocationName { get; set; } = default!;
        public int Capacity { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        // Available items inside the warehose
        public List<InventoryItem> InventoryItems { get; set; } = new();
    }
}
