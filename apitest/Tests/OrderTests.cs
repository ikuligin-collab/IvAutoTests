using apitest.DTO.FilesDTO;
using System.Text.Json;

namespace apitest;

public class OrderTests
{
    private OrderDataDTO order;
    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Orders.json");
        string data = File.ReadAllText(path);
        order = JsonSerializer.Deserialize<OrderDataDTO>(data);
    }
}