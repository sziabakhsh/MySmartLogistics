using Microsoft.EntityFrameworkCore;
using MyLogistics.Application.Interfaces;
using MyLogistics.Domain.Logistics.Entities;
using MyLogistics.Domain.Ordering.Entities;
using System.Reflection;

namespace MyLogistics.Infrastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Main DbSet for Order Aggregate Root
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Shipment> Shipments => Set<Shipment>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seting the throughput for the entire database to 400 RU/s (shared across all containers)
            modelBuilder.HasManualThroughput(400);

            // Automatically apply configurations (like ToContainer, HasPartitionKey, OwnsOne) from Infrastructure
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // SaveChangesAsync is already provided natively by EF Core DbContext
    }
}
