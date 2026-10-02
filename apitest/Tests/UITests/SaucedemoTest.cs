using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.UITests;

public class SaucedemoTest : BaseTest
{
    [Test]
    public async Task LoginTest()
    {
        await Page.GotoAsync("https://www.saucedemo.com");
        //Нахожу и заполняю поле Usrname
        var usernameInput = Page.GetByPlaceholder("Username");
        await usernameInput.FillAsync("standard_user");
        //Нахожу и заполняю поле Password
        var passwordInput = Page.GetByPlaceholder("Password");
        await passwordInput.FillAsync("secret_sauce");
        // Нахожу и нажимаю кнопку Login
        var loginButton = Page.Locator("[data-test='login-button']");
        await loginButton.ClickAsync();
        //Нахожу элемент Produkt
        var titleElement = Page.GetByText("Products");
        //Проверяю, что найденный элемент видим
        var isVisible = await titleElement.IsVisibleAsync();
        isVisible.Should().BeTrue();
    }
}