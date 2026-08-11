using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Tenancy.Entities
{
    public class TenantSettings
    {
        public string Currency { get; set; } = "USD";
        public int MaxOrdersPerMonth { get; set; }
        public bool EnableRealTimeTracking { get; set; }
    }
}
