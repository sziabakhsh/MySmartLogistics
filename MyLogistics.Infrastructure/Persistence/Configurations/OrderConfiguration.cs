using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLogistics.Domain.Ordering.Entities;

namespace MyLogistics.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Cosmos DB container configuration
            builder.ToContainer("Orders");
            builder.HasKey(o => o.Id);
            builder.HasPartitionKey(o => o.TenantId);

            // Map primary key to standard Cosmos DB "id" JSON property
            builder.Property(o => o.Id).ToJsonProperty("id");

            // Convert Enum properties to String for readability in Cosmos DB Data Explorer
            builder.Property(o => o.Status)
                   .HasConversion<string>();

            // Value Objects mapped as embedded JSON documents
            builder.OwnsOne(o => o.ShippingAddress);
            builder.OwnsOne(o => o.Payment);
            builder.OwnsOne(o => o.TotalAmount);

            // Collections mapped as embedded JSON arrays
            builder.OwnsMany(o => o.Items);
            builder.OwnsMany(o => o.StatusHistory);

            // Optimistic concurrency support using Cosmos DB ETag
            builder.UseETagConcurrency();
        }
    }
}
