using apitest.ForUI.Pages.SauseDemo;
using FluentAssertions;


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
        //Создание страниц
        SaucedemoLoginPage loginPage = new SaucedemoLoginPage(Page); //создаю старницу логина
        InventoryPage inventoryPage = new InventoryPage(Page); // создаётся страница с заказами
        CartPage cartPage = new CartPage(Page); // страница с покупками
        CheckoutPage checkoutPage = new CheckoutPage(Page); //страница с чекаутом
        CheckoutPage2 checkoutPage2 = new CheckoutPage2(Page); // страница с чекуаутом 2
        CheckoutCompletePage checkoutCompletePage = new CheckoutCompletePage(Page); // старница финального чекаута

        //Страница логина
        await loginPage.OpenLoginPageAsync(); // переход на стрницу логина
        await loginPage.VerifyTitleLoginPageAsync("Swag Labs"); // проверяю текст хедера 
        await loginPage.LoginUserAsync("standard_user", "secret_sauce"); // ввод логина/пароля и нажатие кнопки логина


        //Страница продуктов
        await inventoryPage.VerifyTitleInventoryAsync("Products"); // проверка, что хедер видим и имеет текст "Product"
        await inventoryPage.VerifyProductIsVisibleAsync(
            "Sauce Labs Fleece Jacket"); // проверяю, что карточка товара "куртка" есть на странице и видим
        await inventoryPage
            .VerifyProductIsVisibleAsync(
                "Sauce Labs Backpack"); // проверяю, что карточка товара "рюкзак" есть на странице и видим

        await inventoryPage.AddToCartByNameAsync("Sauce Labs Backpack"); // нажимаю кнопку покупки у рюкзака
        await inventoryPage.AddToCartByNameAsync("Sauce Labs Fleece Jacket"); // нажимаю кнопку покупки у куртки

        await inventoryPage.OpenCartAsync(); //кликаю по корзине

        //Страница корзины
        await cartPage.VerifyTitleCartAsync("Your Cart"); // проверка, что хедер видим и имеет текст "Your Cart"
        await cartPage.IsProductInCartAsync(
            "Sauce Labs Fleece Jacket"); // проверяю, что товар "куртка" есть на странице и видим
        await cartPage.IsProductInCartAsync(
            "Sauce Labs Backpack"); // проверяю, что товар "рюкзак" есть на странице и видим

        await cartPage.ClickCheckoutAsync(); //Нажатие кнопки чекаута

        //Страница чекаут 1
        await checkoutPage.VerifyTitleCheckoutAsync(
            "Checkout: Your Information"); // проверка, что хедер видим и имеет текст "Checkout: Your Information"
        await checkoutPage.FillInformationAndContinueAsync("Ivan", "Varan", "11542"); //заполняю поля и нажимаю кнопку

        //Страница чекаут 2
        await checkoutPage2
            .VerifyTitleCheckoutAsync(
                "Checkout: Overview"); // проверка, что хедер видим и имеет текст "Checkout: Overview"
        await checkoutPage2.IsProductInCheckoutPageAsync(
            "Sauce Labs Fleece Jacket"); // проверяю, что товар "куртка" есть на странице и видим
        await checkoutPage2
            .IsProductInCheckoutPageAsync(
                "Sauce Labs Backpack"); // проверяю, что товар "рюкзак" есть на странице и видим
        await checkoutPage2.ClickFinishAsync(); //нажатие кнопки финиш

        //Страница финального чекаута
        await checkoutCompletePage
            .VerifyTitleCheckoutAsync(
                "Checkout: Complete!"); // проверка, что хедер видим и имеет текст "Checkout: Complete!
    }
}