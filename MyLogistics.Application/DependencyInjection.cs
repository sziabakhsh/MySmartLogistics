
using Microsoft.Extensions.DependencyInjection;
using MyLogistics.Application.Services;
using MyLogistics.Application.Interfaces;

namespace MyLogistics.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IWarehouseService,WarehouseService>();
            services.AddScoped<IInventoryService, InventoryService>();

            return services;
        }
    }
}
