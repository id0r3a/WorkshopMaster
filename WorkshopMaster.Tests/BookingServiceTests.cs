using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WorkshopMaster.Application.Bookings;
using WorkshopMaster.Application.ServiceTypes;
using WorkshopMaster.Application.Vehicles;
using WorkshopMaster.Domain.Entities;
using WorkshopMaster.Infrastructure.Persistence;
using Xunit;

namespace WorkshopMaster.Tests;

public class BookingServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateBooking_WithLinkedVehicleAndServiceTypes()
    {
        var (provider, db) = TestServiceFactory.Create();

        var vehicle = await SeedVehicleAsync(db);
        var serviceType1 = await SeedServiceTypeAsync(db, "Diagnosis");
        var serviceType2 = await SeedServiceTypeAsync(db, "Oil change");

        var sut = provider.GetRequiredService<IBookingService>();

        var dto = new CreateBookingDto
        {
            VehicleId = vehicle.Id,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(2),
            Notes = "Test booking",
            ServiceTypeIds = { serviceType1.Id, serviceType2.Id }
        };

        var result = await sut.CreateAsync(dto);

        result.Id.Should().BeGreaterThan(0);
        result.VehicleId.Should().Be(vehicle.Id);
        result.ServiceTypes.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_WithoutFilter_ShouldReturnAllBookings()
    {
        var (provider, db) = TestServiceFactory.Create();
        var vehicle = await SeedVehicleAsync(db);

        await SeedBookingAsync(db, vehicle, "Pending");
        await SeedBookingAsync(db, vehicle, "Completed");

        var sut = provider.GetRequiredService<IBookingService>();

        var filter = new BookingFilter();

        var list = await sut.GetAllAsync(filter);

        list.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_FilterByStatus_ShouldReturnOnlyMatching()
    {
        var (provider, db) = TestServiceFactory.Create();
        var vehicle = await SeedVehicleAsync(db);

        await SeedBookingAsync(db, vehicle, "Pending");
        await SeedBookingAsync(db, vehicle, "Completed");

        var sut = provider.GetRequiredService<IBookingService>();

        var filter = new BookingFilter { Status = "Completed" };

        var list = await sut.GetAllAsync(filter);

        list.Should().HaveCount(1);
        list.Single().Status.Should().Be("Completed");
    }

    [Fact]
    public async Task GetAllAsync_FilterByVehicleRegistration_ShouldReturnOnlyMatching()
    {
        var (provider, db) = TestServiceFactory.Create();

        var vehicle1 = await SeedVehicleAsync(db, "ABC123");
        var vehicle2 = await SeedVehicleAsync(db, "XYZ789");

        await SeedBookingAsync(db, vehicle1, "Pending");
        await SeedBookingAsync(db, vehicle2, "Pending");

        var sut = provider.GetRequiredService<IBookingService>();

        var filter = new BookingFilter
        {
            VehicleRegistrationNumber = "ABC123"
        };

        var list = await sut.GetAllAsync(filter);

        list.Should().HaveCount(1);
        list.Single().VehicleRegistrationNumber.Should().Be("ABC123");
    }

    // helpers
    private static async Task<Vehicle> SeedVehicleAsync(
        AppDbContext db,
        string reg = "TEST123")
    {
        var customer = new Customer
        {
            FullName = "Test Customer",
            Email = "test@example.com",
            PhoneNumber = "0700000000"
        };

        var vehicle = new Vehicle
        {
            RegistrationNumber = reg,
            Brand = "Volvo",
            Model = "V60",
            Year = 2020,
            Customer = customer
        };

        db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();
        return vehicle;
    }

    private static async Task<ServiceType> SeedServiceTypeAsync(
        AppDbContext db,
        string name)
    {
        var st = new ServiceType
        {
            Name = name,
            BasePrice = 1000
        };

        db.ServiceTypes.Add(st);
        await db.SaveChangesAsync();
        return st;
    }

    private static async Task<Booking> SeedBookingAsync(
        AppDbContext db,
        Vehicle vehicle,
        string status)
    {
        var booking = new Booking
        {
            Vehicle = vehicle,
            VehicleId = vehicle.Id,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(1),
            Status = status
        };

        db.Bookings.Add(booking);
        await db.SaveChangesAsync();
        return booking;
    }
}
