using apitest.DTO.UsersDTO;
using FluentAssertions;
using System.Text.Json;
using FluentAssertions.Execution;

namespace apitest;

public class UserDataJsonTests
{
    private UserDataDTO user;
    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
        string data = File.ReadAllText(path);
        user = JsonSerializer.Deserialize<UserDataDTO>(data);
    }

    [Test]
    public void Test21()
    {
      user.UserDto.Should().HaveCount(10);
    }
   
}