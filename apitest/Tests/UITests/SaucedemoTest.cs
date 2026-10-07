using apitest.ForUI.Pages.SauseDemo;
using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.UITests;

public class SaucedemoTest : BaseTest
{
    [Test] //без паттерна Page Object
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

    [Test] // по паттерну Page Object
    public async Task HomeworkTest1()
    {
        SaucedemoLoginPage loginPage = new SaucedemoLoginPage(Page);
        await loginPage.OpenLoginPageAsync(); //создаю старницу логина
        await loginPage.LoginUserAsync("standard_user", "secret_sauce"); // логинусь
        await Assertions.Expect(Page)
            .ToHaveURLAsync(
                "https://www.saucedemo.com/inventory.html"); //проверяю, что после логина открылась старница продуктов

        InventoryPage inventoryPage = new InventoryPage(Page); // создаётся страница с заказами
        string actualTitle = await inventoryPage.GetHeaderTitleTextAsync(); //проверяю зто страница прогрузилась
        Assert.That(actualTitle, Is.EqualTo("Swag Labs"), "Заголовок страницы не совпадает с ожидаемым!");
        bool isProductVisible1 = await inventoryPage.IsProductVisibleAsync("Sauce Labs Fleece Jacket");
        Assert.That(isProductVisible1, Is.True, "Товар 'Sauce Labs Fleece Jacket' не найден на странице!");

        bool isProductVisible2 = await inventoryPage.IsProductVisibleAsync("Sauce Labs Backpack");
        Assert.That(isProductVisible2, Is.True, "Товар 'Sauce Labs Backpack' не найден на странице!");

        await inventoryPage.AddToCartByNameAsync("Sauce Labs Backpack"); // нажимаю кнопку покупки у рюкзака
        await inventoryPage.AddToCartByNameAsync("Sauce Labs Fleece Jacket"); // нажимаю кнопку покупки у куртки

        await inventoryPage.OpenCartAsync(); //кликаю по корзине
        await Assertions.Expect(Page)
            .ToHaveURLAsync("https://www.saucedemo.com/cart.html"); // проверяю, что открылась станица корзины

        CartPage cartPage = new CartPage(Page); // страница с покупками

        // Проверяю наличие куртки
        bool isJacketVisible = await cartPage.IsProductInCartAsync("Sauce Labs Fleece Jacket");
        Assert.That(isJacketVisible, Is.True, "Куртка не найдена в корзине");

        // Проверяю наличие рюкзака
        bool isBackpackVisible = await cartPage.IsProductInCartAsync("Sauce Labs Backpack");
        Assert.That(isBackpackVisible, Is.True, "Рюкзак не найден в корзине");

        await cartPage.ClickCheckoutAsync();

        CheckoutPage checkoutPage = new CheckoutPage(Page); //страница с чекаутом
        bool isTitleVisible = await checkoutPage.IsTitleVisibleAsync();
        Assert.That(isTitleVisible, Is.True, "Заголовок 'Checkout: Your Information' не отображается!");

        await checkoutPage.FillInformationAndContinueAsync("Ivan", "Varan", "11542");

        CheckoutPage2 checkoutPage2 = new CheckoutPage2(Page);
        bool isTitleVisible2 = await checkoutPage2.IsTitleVisibleAsync();
        Assert.That(isTitleVisible2, Is.True, "Заголовок 'Checkout: Overview' не отображается!");
        // Проверяю наличие куртки
        bool hasJacketVisible = await checkoutPage2.IsProductInChecoutPageAsync("Sauce Labs Fleece Jacket");
        Assert.That(hasJacketVisible, Is.True, "Куртка не найдена на странице чекаута");

        // Проверяю наличие рюкзака
        bool hasBackpackVisible = await checkoutPage2.IsProductInChecoutPageAsync("Sauce Labs Backpack");
        Assert.That(hasBackpackVisible, Is.True, "Рюкзак не найден на странице чекаута");

        await checkoutPage2.ClickFinishAsync();

        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/checkout-complete.html");

        CheckoutCompletePage checkoutCompletePage = new CheckoutCompletePage(Page);
        string actualText = await checkoutCompletePage.GetHeaderTextAsync(); //получаю текст хедера
        Assert.That(actualText, Is.EqualTo("Thank you for your order!")); //сравниваю полученный текст хедера с ОР
    }
}