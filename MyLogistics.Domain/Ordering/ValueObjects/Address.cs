using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Ordering.ValueObjects
{
    // Represents a physical address for shipping or billing purposes
    public class Address
    {
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string StateOrProvince { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string RecipientName { get; set; } = default!;
        public string RecipientPhoneNumber { get; set; } = default!;
    }
}
