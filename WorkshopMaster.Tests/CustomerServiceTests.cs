using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WorkshopMaster.Application.Common;
using WorkshopMaster.Application.Customers;
using Xunit;

namespace WorkshopMaster.Tests;

public class CustomerServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldPersistCustomer()
    {
        var (provider, _) = TestServiceFactory.Create();
        var sut = provider.GetRequiredService<ICustomerService>();

        var dto = new CreateCustomerDto
        {
            FullName = "Anna Svensson",
            Email = "anna@example.com",
            PhoneNumber = "0701111111"
        };

        var created = await sut.CreateAsync(dto);

        created.Id.Should().BeGreaterThan(0);
        created.FullName.Should().Be("Anna Svensson");
    }

    [Fact]
    public async Task CreateAsync_DuplicateEmail_ShouldThrowCustomerAlreadyExistsException()
    {
        var (provider, _) = TestServiceFactory.Create();
        var sut = provider.GetRequiredService<ICustomerService>();

        var dto = new CreateCustomerDto
        {
            FullName = "First",
            Email = "duplicate@example.com",
            PhoneNumber = "0701111111"
        };

        await sut.CreateAsync(dto);

        Func<Task> act = async () => await sut.CreateAsync(new CreateCustomerDto
        {
            FullName = "Second",
            Email = "duplicate@example.com",
            PhoneNumber = "0702222222"
        });

        await act.Should().ThrowAsync<CustomerAlreadyExistsException>();
    }
}
