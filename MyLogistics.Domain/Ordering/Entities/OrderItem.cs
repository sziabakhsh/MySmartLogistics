using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Ordering.Entities
{
    // آیتم‌های داخل سفارش
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public string SKU { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string Category { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<string> Attributes { get; set; } = new(); // e.g., ["Color:Red", "Size:XL"]
    }
}
