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
      user.UserDto.Count().Should().Be(10);
    }

    [Test]
    public void Test22()
    {
        user.UserDto.First().Profile.FullName.Should().Be("Alice Johnson");
    }

    [Test]
    public void Test23()
    {
        var ids = user.UserDto.Select(u => u.Id);
        ids.Should().OnlyHaveUniqueItems();
    }

    [Test]
    public void Test24()
    {
        var hasPremium = user.UserDto.Any(x=>x.Profile.Tags.Contains("premium"));
        hasPremium.Should().BeTrue();
    }

    [Test]
    public void Test25()
    {
        var HaveCity = user.UserDto.All(u => !string.IsNullOrWhiteSpace(u.Profile.Address.City));
        HaveCity.Should().BeTrue();
    }

    [Test]
    public void Test26()
    {
        var stockholmec = user.UserDto.Any(u => u.Profile.Address.City == "Stockholm");
        stockholmec.Should().BeTrue();
    }

    [Test]
    public void Test27()
    { 
        user.UserDto.Should().AllSatisfy(u => 
            u.Profile.Age.Should().BeInRange(18, 60));
        
    }

    [Test]
    public void Test28()
    { 
        var hasAdmin = user.UserDto.Any(u => u.Roles.Contains("admin"));
        hasAdmin.Should().BeTrue();
    }
}