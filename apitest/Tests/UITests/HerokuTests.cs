using apitest.ForUI.Pages;
using Microsoft.Playwright;
using FluentAssertions;

namespace apitest.UITests;

public class HerokuTests : BaseTest
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
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        loginPage.LoginUserAsync("wrong", "wrong");
        var errorMessage = await loginPage.GetTextFromErrorMessageAsync();
        errorMessage.Should().Contain("Your username is invalid");
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

    [Test]
    //нестандартный дробдаун
    public async Task Should_Select_Sub_Items()
    {
        await Page.GotoAsync("https://demoqa.com/select-menu");
        var dropdown = Page.Locator("#withOptGroup");
        await dropdown.ClickAsync();

        var option = Page.GetByText("Group 1, option 1");
        await option.ClickAsync();

        var text = await dropdown.TextContentAsync();
        await Assertions.Expect(dropdown).ToContainTextAsync("Group 1, option 1");
    }

    [Test]
    public async Task AddRemoveElements()
    {
       AddRemovePage addRemovePage = new AddRemovePage(Page);
        // Открываем страницу
       await addRemovePage.OpenAddRemovePage();
       await addRemovePage.CheckPageOpenAsync();
       
        // --- ДЕЙСТВИЕ 1: добавить первую кнопку ---
        await addRemovePage.ClickButtonByNameAsync("Add Element");

        // Проверка: появилась 1 кнопка Delete
        await addRemovePage.CheckNumberOfButtonAsync("Delete", 1);

        // --- ДЕЙСТВИЕ 2: добавить вторую кнопку ---
        await addRemovePage.ClickButtonByNameAsync("Add Element");

        // Проверка: теперь их 2 
        await addRemovePage.CheckNumberOfButtonAsync("Delete", 2);

        // --- ДЕЙСТВИЕ 3: удалить одну кнопку ---
        await addRemovePage.ClickButtonByNameAndIndexAsync("Delete", 2);

        // Проверка: осталась 1 кнопка
        await addRemovePage.CheckNumberOfButtonAsync("Delete", 1);
    }
}