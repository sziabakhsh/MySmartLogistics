using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyLogistics.WebAPI.Filters
{
    public class TenantHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Tenant-Id",
                In = ParameterLocation.Header,
                Required = true,
                Description = "Tenant ID for data isolation in Multi-Tenancy",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("t01")
                }
            });
        }
    }
}
