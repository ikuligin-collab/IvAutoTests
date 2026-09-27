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
    
    [Test]
    public async Task AccessoriesByUsersFromDifferentCities()
    {
        //Достаю категорию "Аксессуары" c Id = 6
        var category = await p.Provider.GetRequiredService<ICategoriesRepository>().GetCategoriesByIdAsync(6);
        category.Should().NotBeNull();

        //Достаю ID всех товаров из этой категории
        var categoryProducts = await p.Provider.GetRequiredService<IProductsRepository>().GetProductByCategoryIDAsync(category.Id);
        var accessoryIds = categoryProducts.Select(p => p.Id).ToHashSet();

        //Достаю все заказы и позицию товаров в заказах
        var orders = await p.Provider.GetRequiredService<IOrdersRepository>().GetAllOrdersAsync();
        var orderItems = await p.Provider.GetRequiredService<IOrderItemsRepository>().GetAllOrderItemsAsync();

        //Фильтрую ID заказов, в которых есть аксессуары
        var accessoryOrderIds = orderItems
            .Where(item => accessoryIds.Contains(item.ProductId))
            .Select(item => item.OrderId)
            .ToHashSet();

        //Собираю уникальные ID пользователей из этих заказов
        var userIds = orders
            .Where(order => accessoryOrderIds.Contains(order.Id))
            .Select(order => order.UserId)
            .Distinct();

        //Достаю города этих пользователей
        var addressesRepo = p.Provider.GetRequiredService<IAddressRepository>();
        var cities = new HashSet<string>();

        foreach (var userId in userIds)
        {
            var address = await addressesRepo.GetAddressesByUserIdAsync(userId);
            if (address != null)
            {
                cities.Add(address.City);
            }
        }

        //Проверка: городов больше одного
        cities.Count.Should().BeGreaterThan(1);
    }
}