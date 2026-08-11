using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MyLogistics.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Read Cosmos DB configuration from User Secrets or AppSettings
            var cosmosEndpoint = configuration["CosmosDb:Endpoint"];
            var cosmosKey = configuration["CosmosDb:AccountKey"];
            var databaseName = configuration["CosmosDb:DatabaseName"] ?? "SmartLogisticsDb";

            // Register EF Core DbContext with Azure Cosmos DB Provider
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseCosmos(
                    accountEndpoint: cosmosEndpoint!,
                    accountKey: cosmosKey!,
                    databaseName: databaseName,
                    cosmosOptions =>
                    {
                        cosmosOptions.RequestTimeout(TimeSpan.FromSeconds(15));
                    });
            });
            return services;
        }
    }
}
