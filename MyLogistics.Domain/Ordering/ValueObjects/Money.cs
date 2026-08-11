using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Ordering.ValueObjects
{
    // Financial value encapsulation following the Money Pattern
    public class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD"; // Standard ISO currency codes (e.g., USD, CAD, EUR)

        public Money() { }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
