using Microsoft.EntityFrameworkCore;
using MyLogistics.Domain.Logistics.Entities;
using MyLogistics.Domain.Ordering.Entities;

namespace MyLogistics.Application.Interfaces
{
    public interface IAppDbContext
    {
        // DbSet for Entities Aggregate Root
        DbSet<Order> Orders { get; }
        //DbSet<Shipment> Shipments { get; }

        // Save changes to the underlying database
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
