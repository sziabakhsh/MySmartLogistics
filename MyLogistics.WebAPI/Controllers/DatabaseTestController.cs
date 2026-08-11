using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLogistics.Domain.Ordering.Entities;
using MyLogistics.Domain.Ordering.Enums;
using MyLogistics.Infrastructure;

namespace MyLogistics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public DatabaseTestController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <summary>
        /// Inserts a sample order document into Azure Cosmos DB
        /// </summary>
        [HttpPost("create-sample-order")]
        public async Task<IActionResult> CreateSampleOrder(CancellationToken ct)
        {
            var testOrder = new Order
            {
                Id = Guid.NewGuid(),
                TenantId = "tenant-vancouver-01",
                OrderNumber = $"ORD-{DateTime.UtcNow.Ticks.ToString()[^6..]}",
                CreatedAtUtc = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            _dbContext.Orders.Add(testOrder);
            await _dbContext.SaveChangesAsync(ct);

            return Ok(new
            {
                Message = "Document successfully created in Azure Cosmos DB!",
                OrderId = testOrder.Id,
                PartitionKey = testOrder.TenantId
            });
        }

    }
}
