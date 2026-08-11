using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Ordering.Enums
{
    public enum OrderStatus
    {
        Pending = 1,          // Order created, awaiting payment
        PaymentFailed = 2,    // Payment transaction failed
        Processing = 3,       // Payment confirmed, order being prepared in warehouse
        Shipped = 4,          // Package handed over to logistics carrier
        Delivered = 5,        // Order successfully delivered to customer
        Cancelled = 6,        // Order cancelled by customer or system
        Refunded = 7          // Order returned and payment refunded
    }
}
