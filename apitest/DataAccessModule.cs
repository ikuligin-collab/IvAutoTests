using apitest.DapperRepository;
using apitest.Interfaces.DapperInterface;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository>(p => new UserRpository(connectionString));
        services.AddScoped<IAddressRepository>(p => new AddressRpository(connectionString));
        return services;
    }
}