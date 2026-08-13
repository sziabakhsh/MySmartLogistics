using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Logistics.Entities
{
    public class InventoryItem
    {
        public Guid Id { get; set; }

        // Partition Key
        public string TenantId { get; set; } = default!;

        // Foreign key
        public Guid WarehouseId { get; set; }

        public string Sku { get; set; } = default!; // Stock Keeping Unit
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; } // Minimum availablity items
        public decimal UnitPrice { get; set; }
        public DateTime LastUpdatedUtc { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Warehouse? Warehouse { get; set; }
    }
}