using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Application.Interfaces
{
    public interface ITenantProvider
    {
        string GetTenantId();
    }
}
