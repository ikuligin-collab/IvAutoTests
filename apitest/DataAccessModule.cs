using apitest.DapperRepository;
using apitest.Interfaces.DapperInterface;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IAddressRepository>(p => new AddressRpository(connectionString));
        services.AddScoped<ICategoriesRepository>(p => new CategoriesRpository(connectionString));
        services.AddScoped<IOrderItemsRepository>(p => new OrderItemsRepository(connectionString));
        services.AddScoped<IOrdersRepository>(p => new OrdersRepository(connectionString));
        services.AddScoped<IProductsRepository>(p => new ProductsRepository(connectionString));
        services.AddScoped<IReviewsRepository>(p => new ReviewsRepository(connectionString));
        services.AddScoped<IUserRepository>(p => new UserRpository(connectionString));
        return services;
    }
}