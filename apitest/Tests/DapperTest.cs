using apitest.Interfaces.DapperInterface;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class DapperTest
{
    private readonly TestPrecondition p = new TestPrecondition();
    //[Test]
    public async Task Initialize()
    {
        var connectionString = "Data Source=marketplace.db";
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();
        await DatabaseInitializer.InitializeAsync(connection);
        
    }

    [Test]
    public async Task GetAllUsers()
    {
        var repo = p.Provider.GetRequiredService<IUserRepository>();
        var users = await repo.GetAllAsync();
        users.Should().HaveCount(15);
    }

    [Test]
    public async Task GetUserById()
    {
        var repo = p.Provider.GetRequiredService<IUserRepository>();
        var user = await repo.GetByIdAsync(1);
        user.Should().NotBeNull();
    }

    [Test]
    public async Task GetAllAddresses()
    {
        var repo = p.Provider.GetRequiredService<IAddressRepository>();
        var addresses = await repo.GetAllAddressesAsync();
        addresses.Should().HaveCount(15);
    }

    [Test]
    public async Task GetAddressById()
    {
        var repo = p.Provider.GetRequiredService<IAddressRepository>();
        var address = await repo.GetAddressesByUserIdAsync(14);
        address.Should().NotBeNull();

    }

    [Test]
    public async Task GetUserByFirstAndLastName()
    {
        var repo1 = p.Provider.GetRequiredService<IUserRepository>();
        var user = await repo1.GetUserByFirstAndLastName("Елена", "Кузнецова");
        var repo2 = p.Provider.GetRequiredService<IAddressRepository>();
        var address = await repo2.GetAddressesByUserIdAsync(user.Id);
        address.City.Should().Be("Казань");
    }
}