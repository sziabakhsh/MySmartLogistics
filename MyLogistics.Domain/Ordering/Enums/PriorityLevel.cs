using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Ordering.Enums
{
    public enum PriorityLevel
    {
        Low = 1,
        Standard = 2,
        High = 3,
        Express = 4           // Requires expedited processing and dispatch
    }
}
