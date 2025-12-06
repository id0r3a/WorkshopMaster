using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WorkshopMaster.Application.Vehicles;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;
using Xunit;

namespace WorkshopMaster.Tests;

public class VehicleServiceTests
{
    [Fact]
    public async Task GetByRegistrationAsync_ShouldReturnCorrectVehicle()
    {
        var (provider, db) = TestServiceFactory.Create();

        var customer = new Customer
        {
            FullName = "Vehicle Tester",
            Email = "veh@example.com",
            PhoneNumber = "0703333333"
        };

        var vehicle = new Vehicle
        {
            RegistrationNumber = "VTEST123",
            Brand = "BMW",
            Model = "320",
            Year = 2019,
            Customer = customer
        };

        db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();

        var sut = provider.GetRequiredService<IVehicleService>();

        var dto = await sut.GetByRegistrationAsync("VTEST123");

        dto.Should().NotBeNull();
        dto!.RegistrationNumber.Should().Be("VTEST123");
        dto.Brand.Should().Be("BMW");
    }
}
