using apitest.Interfaces.DapperInterface;
using FluentAssertions;
using FluentAssertions.Execution;
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
    public async Task GetAddressByFirstAndLastName()
    {
        var repo1 = p.Provider.GetRequiredService<IUserRepository>();
        var user = await repo1.GetUserByFirstAndLastName("Елена", "Кузнецова");
        var repo2 = p.Provider.GetRequiredService<IAddressRepository>();
        var address = await repo2.GetAddressesByUserIdAsync(user.Id);
        address.City.Should().Be("Казань");
    }

    [Test]
    public async Task GetAllCategories()
    {
        var repo = p.Provider.GetRequiredService<ICategoriesRepository>();
        var cat = await repo.GetAllCategoriesAsync();
        cat.Should().HaveCount(6);
    }

    [Test]
    public async Task GetProduct()
    {
        var repo = p.Provider.GetRequiredService<IProductsRepository>();
        var product = await repo.GetProductByIdAsync(2);
        using (new AssertionScope())
        {
            product.Name.Should().Be("Samsung Galaxy S24");
            product.Description.Should().Be("Флагманский смартфон Samsung");
            product.Price.Should().Be(69990);
            product.Stock.Should().Be(20);
            product.CategoryId.Should().Be(1);  
        }
    }
    
    [Test]
    public async Task GetOrderByUserId()
    {
        var order = p.Provider.GetRequiredService<IOrdersRepository>();
        var userOdred = await order.GetOrderByUserIdAsync(2); // получил объект заказа конкретного юзера 
        userOdred.Should().NotBeNull(); // проверяю, что достал объект
        var orderItem = p.Provider.GetRequiredService<IOrderItemsRepository>();
        var userOdredItem = await orderItem.GetOrderItemById(userOdred.Id); //получил объект конкретного заказа
        userOdredItem.Should().NotBeNull();// проверяю, что достал объект конкретного заказа
        var products = p.Provider.GetRequiredService<IProductsRepository>();
        var userProduct = await products.GetProductByIdAsync(userOdredItem.Id); //получил объект продукта
        userProduct.Name.Should().Be("Samsung Galaxy S24");
        
    }
}