using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLogistics.Application.Interfaces;

namespace MyLogistics.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
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
                    databaseName: databaseName
                );
            });

            // Register EF Core DbContext with Azure Cosmos DB Provider
            services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

            return services;
        }
    }
}
