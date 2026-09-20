using apitest.DTO.BookStoreDTO;
using apitest.Interfaces.BookStore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace apitest;

public class BookStoreTests
{
    private IBookStore api;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddRefitClient<IBookStore>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://demoqa.com");
            });
        
        var provider = services.BuildServiceProvider();
        api = provider.GetRequiredService<IBookStore>();
    }

    [Test]
    public async Task CreateUser()
    {
        var user = new CreateUserBookStoreDTO{UserName = "Ivan1", Password = "StrongPass123!"};
        var response = await api.CreateUserAsync(user);
        // Айди моего юзера 222696dd-ac7a-4787-ab2c-a0abb54340ec 
    }

    [Test]
    public async Task GetToken()
    {
        var user = new CreateUserBookStoreDTO() {UserName = "Ivan1", Password = "StrongPass123!"};
        var token = await api.GenerateTokenAsync(user);
        token.Token.Should().NotBeEmpty();
        token.Status.Should().Be("Success");
        token.Result.Should().Contain("authorized");
    }
    
}