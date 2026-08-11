using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLogistics.Domain.Logistics.Entities;

namespace MyLogistics.Infrastructure.Persistence.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            // Cosmos DB container name and partition key configuration
            builder.ToContainer("Shipments");
            builder.HasPartitionKey(s => s.TenantId);
            builder.HasKey(s => s.Id);

            // Value Object mapped as embedded JSON document
            builder.OwnsOne(s => s.CurrentLocation);

            // Collection mapped as embedded JSON array
            builder.OwnsMany(s => s.RouteCheckpoints);
        }
    }
}
