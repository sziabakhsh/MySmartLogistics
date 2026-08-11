using Microsoft.EntityFrameworkCore;
using MyLogistics.Domain.Logistics.Entities;
using MyLogistics.Domain.Ordering.Entities;
using System.Reflection;

namespace MyLogistics.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Main DbSet for Order Aggregate Root
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Shipment> Shipment => Set<Shipment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatically applies all IEntityTypeConfiguration classes found in Infrastructure assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
