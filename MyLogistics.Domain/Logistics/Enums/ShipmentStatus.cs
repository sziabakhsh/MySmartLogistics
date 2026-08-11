using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogistics.Domain.Logistics.Enums
{
    public enum ShipmentStatus
    {
        Created = 1,          // Shipping label generated
        InTransit = 2,        // Package in transit between hubs/vehicles
        OutForDelivery = 3,   // Courier actively attempting delivery
        Delivered = 4,        // Package delivered to final destination
        FailedAttempt = 5,    // Unsuccessful delivery attempt
        ReturnedToWarehouse = 6 // Shipment returned to origin facility
    }
}
