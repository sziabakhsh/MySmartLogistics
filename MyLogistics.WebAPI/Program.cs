using Microsoft.OpenApi.Models;
using MyLogistics.Application;
using MyLogistics.Application.Interfaces;
using MyLogistics.Infrastructure;
using MyLogistics.Infrastructure.Health;
using MyLogistics.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);


// 1. Register Infrastructure (Provides IAppDbContext implementation)
builder.Services.AddInfrastructureServices(builder.Configuration);

//builder.Services.AddScoped<IAppDbContext>(provider =>
//    provider.GetRequiredService<AppDbContext>());

// 2. Register Application (Provides IOrderService implementation)
builder.Services.AddAppServices();

// 3. Register Health Check
builder.Services.AddHealthChecks()
    .AddCheck<CosmosDbHealthCheck>(
        name: "cosmosdb",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: new[] { "db", "azure", "cosmos" });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MyLogistics API", Version = "v1" });

    // add a custom header parameter for tenant ID in Swagger UI
    options.OperationFilter<TenantHeaderOperationFilter>();
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 4. Map Health Check Endpoint (Built-in extension method)
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
