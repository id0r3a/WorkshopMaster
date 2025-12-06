using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkshopMaster.Application.Bookings;
using WorkshopMaster.Application.Common;
using WorkshopMaster.Application.Customers;
using WorkshopMaster.Application.Dashboard;
using WorkshopMaster.Application.ServiceTypes;
using WorkshopMaster.Application.Vehicles;
using WorkshopMaster.Infrastructure.Persistence;
using WorkshopMaster.Infrastructure.Services;

namespace WorkshopMaster.Tests;

public static class TestServiceFactory
{
    public static (ServiceProvider Provider, AppDbContext Db) Create()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        });

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IServiceTypeService, ServiceTypeService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingStatsService, BookingStatsService>();

        var provider = services.BuildServiceProvider();
        var db = provider.GetRequiredService<AppDbContext>();

        return (provider, db);
    }
}
