using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Ordering.ValueObjects
{
    // Transaction and payment metadata
    public class PaymentDetails
    {
        public string PaymentMethod { get; set; } = default!; // e.g., "CreditCard", "PayPal", "Crypto"
        public string TransactionId { get; set; } = default!;
        public bool IsPaid { get; set; }
        public DateTime? PaidAtUtc { get; set; }
        public string? GatewayProvider { get; set; }          // e.g., "Stripe", "PayPal"
    }
}
