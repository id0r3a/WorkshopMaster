using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WorkshopMaster.Application.Dashboard;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;
using Xunit;

namespace WorkshopMaster.Tests
{
    public class DashboardStatsTests
    {
        [Fact]
        public async Task GetStatsAsync_ShouldReturnValues_WhenThereAreBookings()
        {
            // Arrange
            var (provider, db) = TestServiceFactory.Create();

            var customer = new Customer
            {
                FullName = "Dash Test",
                Email = "dash@example.com",
                PhoneNumber = "0700000000"
            };

            var vehicle = new Vehicle
            {
                RegistrationNumber = "DASH123",
                Brand = "Volvo",
                Model = "XC60",
                Year = 2022,
                Customer = customer
            };

            db.Vehicles.Add(vehicle);

            db.Bookings.Add(new Booking
            {
                Vehicle = vehicle,
                VehicleId = vehicle.Id,
                StartTime = DateTime.UtcNow.AddDays(-1),
                EndTime = DateTime.UtcNow,
                Status = "Pending"
            });

            await db.SaveChangesAsync();

            var sut = provider.GetRequiredService<IBookingStatsService>();

            // Act
            var stats = await sut.GetStatsAsync();

            // Assert
            stats.Should().NotBeNull();

            stats.OpenOrders.Should().BeGreaterThan(0);
            stats.TotalCustomers.Should().BeGreaterThan(0);
        }
    }
}
