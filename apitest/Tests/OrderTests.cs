using apitest.DTO.FilesDTO;
using FluentAssertions;
using System.Text.Json;
using FluentAssertions.Execution;

namespace apitest;

public class OrderTests
{
    private OrderDataDTO order;
    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "OrderData.json");
        string data = File.ReadAllText(path);
        order = JsonSerializer.Deserialize<OrderDataDTO>(data);
    }

    [Test]
    public void Test1()
    {
        foreach (var item in order.Items)
        {
            TestContext.WriteLine($" {item.ProductId} | {item.Quantity} | {item.Price}");
        }
        order.Items.Should().NotBeEmpty();
        order.Items.Should().HaveCount(3);
    }

    [Test]
    public void Test2()
    {
        var sum = order.Items.Select(x => x.Quantity * x.Price).Sum();
        var expectedSum = order.Summary.ItemsTotal;
        sum.Should().Be(expectedSum);
    }

    [Test]
    public void Test3()
    {
        var electronicsItems = order.Items.Where(x=>x.Category == "Electronics").ToList();
        foreach (var item in electronicsItems)
        {
            TestContext.WriteLine($"Электроника: {item.Name} ");
        }
        electronicsItems.Should().OnlyContain(x=>x.Category=="Electronics");
    }

    [Test]
    public void Test4()
    {
        order.Payment.Status.Should().Be("paid");
    }

    [Test]
    public void Test5()
    {
        var mostExpensiveItem = order.Items.OrderByDescending(x=>x.Price).First();
      //Проверка нескольких полей через анонимные типы
       mostExpensiveItem.Should().BeEquivalentTo(new
       {
            Price = 129.99,
            Name = "Wireless Headphones"
       });
    }
    [Test]
    public void Test6()
    {
        var listofItems = order.Items.Where(x=>x.Price > 50).ToList();
        foreach (var item in listofItems)
        {
            TestContext.WriteLine($"{item.ProductId} | {item.Quantity} | {item.Price}");   
        }
        listofItems.Should().NotBeEmpty();
    }
}