using Microsoft.Playwright;
using FluentAssertions;

namespace apitest.UITests;

public class HerokuTests:BaseTest
{
    [Test]
    public async Task CheckBoxtest()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
            var first = Page.Locator("input[type='checkbox']").Nth(0);
            await first.ClickAsync();
            (await first.IsCheckedAsync()).Should().BeTrue();

    }

    [Test]
    public async Task FormAuthentification()
    {
      await Page.GotoAsync("https://the-internet.herokuapp.com/login");  
      //var loginElement = Page.Locator("//input[@id='username']"); // ищем эдемент по хпасу
      var loginElement = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
      await loginElement.FillAsync("wrong");
      var passwordElement = Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
      await passwordElement.FillAsync("wrong");
      //var loginButton = await Page.QuerySelectorAsync("button[type='submit']");
      var loginButton = Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
      await loginButton.ClickAsync();
      //проверим наличие errorMessage способ 1
      var errorMessage = Page.Locator("//div[contains(text(), 'Your username is invalid!')]");
      var state = await errorMessage.IsVisibleAsync();
      state.Should().BeTrue();
      //проверим наличие errorMessage способ 2
      //var errorMessage2 = await Page.QuerySelectorAsync("#flash");
      var errorMessage2 = Page.GetByText("Your username is invalid!");
      var textErrorMessage = await errorMessage2.InnerTextAsync(); // возвращает текст всего контейнера, даже с крестиком закрытия
      textErrorMessage.Should().Contain("Your username is invalid!"); // использщовал миенно контейн, название соджержит не только текст 
    }
}