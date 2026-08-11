using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyLogistics.Infrastructure.Health
{
    public class CosmosDbHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public CosmosDbHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var endpoint = _configuration["CosmosDb:Endpoint"];
                var accountKey = _configuration["CosmosDb:AccountKey"];

                if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(accountKey))
                {
                    return HealthCheckResult.Unhealthy("Cosmos DB credentials are missing in User Secrets or configuration.");
                }

                // Create a temporary client to test the actual connection to the Azure account
                using var client = new CosmosClient(endpoint, accountKey);

                // Calling the ReadAccountAsync method to verify the key and endpoint on the Azure server
                await client.ReadAccountAsync();

                return HealthCheckResult.Healthy("Connection to Azure Cosmos DB is healthy.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Failed to connect to Azure Cosmos DB.", ex);
            }
        }
    }
}
