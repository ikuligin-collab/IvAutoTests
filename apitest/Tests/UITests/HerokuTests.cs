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

    [Test]
    public async Task TestDropdown()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/dropdown");
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/dropdown");
        var dropdown = Page.Locator("#dropdown");
        await Assertions.Expect(dropdown).ToBeVisibleAsync();
        
        await dropdown.SelectOptionAsync("1"); // значение атрибута
        //#1
        await Assertions.Expect(dropdown).ToHaveValueAsync("1");
        //запонить для стандартного дробдауна
        //#2
        var selested = dropdown.Locator("option:checked");
        await Assertions.Expect(selested).ToHaveTextAsync("Option 1");
        //универслаьная проверка
        //#3
        var text = await dropdown.InnerTextAsync();
        text.Should().Contain("Option 1");
        //#4
        var opt1 = Page.Locator("//option[@selected='selected']");
        var textOpt1 = await opt1.InnerTextAsync();
        textOpt1.Should().Be("Option 1");
        
       //select option 2
        await dropdown.SelectOptionAsync("2"); // значение атрибута
        await Assertions.Expect(dropdown).ToHaveValueAsync("1");
        //запонить для стандартного дробдауна
        //var selested1 = dropdown.Locator("option:checked");
        await Assertions.Expect(selested).ToHaveTextAsync("Option 1");
        //универслаьная проверка
        var text2 = await dropdown.InnerTextAsync();
        text2.Should().Contain("Option 2");
        var opt2 = Page.Locator("//option[@selected='selected']");
        var textOpt2 = await opt2.InnerTextAsync();
        textOpt2.Should().Be("Option 2");
        
        //ннестандартный дробдаун
        await dropdown.ClickAsync();
        var option2 = Page.Locator("//option[text()='Option 2']");
        await option2.ClickAsync();
        var textFromDropdown = await dropdown.InnerTextAsync();
        textFromDropdown.Should().Be("Option 2");
        
    }
}