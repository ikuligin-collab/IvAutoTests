using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using apitest.DTO;

namespace apitest;

public class Tests
{
    private static HttpClient client;

    [OneTimeSetUp]
    public void Setup()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3IMkASnOWZzNwN8EVQkJPchdUZQ");
    }

    [Test]
    public async Task Test1()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public  async Task Test2()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        string json = await response.Content.ReadAsStringAsync();
        UserResponseDTO userResponse = JsonSerializer.Deserialize<UserResponseDTO>(json);
        UserDataDTO user = userResponse.Data;
        if(user.Id ==2)
        { 
            
        }
        else
        {
            throw new Exception();
        }
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
}
//токен free_user_3IMkASnOWZzNwN8EVQkJPchdUZQ