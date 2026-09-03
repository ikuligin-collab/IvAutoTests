using System.Net;
using apitest.Interfaces;
using apitest.DTO;
using Refit;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class RefitTests
{
    private IUserApiClient _client;
    
    [OneTimeSetUp]
    public void Setup()
    {
       var services = new ServiceCollection();
       services.AddRefitClient<IUserApiClient>()
           .ConfigureHttpClient(c =>
           {
               c.BaseAddress = new Uri("http://reqres.in/api/");
           });
       var provider = services.BuildServiceProvider();
       _client = provider.GetRequiredService<IUserApiClient>();

    }
    [Test]
    public async Task Test1()
    {
        var result = await _client.GetUserAsync(2);
        Assert.That(result.Data.Id, Is.EqualTo(2));
        Assert.That(result.Data.Email, Is.Not.Null);
    }
    
    [Test]
    public async Task Test2()
    {
        var newUser = new CreateUserRequestDTO("Alex", "Samsung");
        var response = await _client.PostUserAsync(newUser);
        Assert.That(response.Name, Is.EqualTo("Alex"));
    }
    
    [Test]
    public async Task Test3()
    {
        var updateUser = new CreateUserRequestDTO("Alex", "Apple");
        var response = await _client.PutUserAsync(2, updateUser);
        Assert.That(response.Job, Is.EqualTo("Apple"));
    }
    
    [Test]
    public async Task Test4()
    {
        var response = await _client.DeleteUserAsync(2);
        Assert.That(response.StatusCode, expression:Is.EqualTo(HttpStatusCode.NoContent));
    }
}