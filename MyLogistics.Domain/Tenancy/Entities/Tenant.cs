using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Tenancy.Entities
{
    public class Tenant
    {
        public string Id { get; set; } = default!; // Same as TenantId (Partition Key)
        public string Name { get; set; } = default!;
        public string Tier { get; set; } = default!; // "Basic", "Enterprise"
        public bool IsActive { get; set; }
        public DateTime JoinedAtUtc { get; set; }
        public TenantSettings Settings { get; set; } = default!;
    }
}
