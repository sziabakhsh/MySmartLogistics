using MyLogistics.Infrastructure;
using MyLogistics.Infrastructure.Health;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

// 1. Register Health Check
builder.Services.AddHealthChecks()
    .AddCheck<CosmosDbHealthCheck>(
        name: "cosmosdb",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: new[] { "db", "azure", "cosmos" });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 2. Map Health Check Endpoint (Built-in extension method)
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
