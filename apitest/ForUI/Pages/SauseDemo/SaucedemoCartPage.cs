using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace apitest.ForUI.Pages.SauseDemo;

public class CartPage
{
    private readonly IPage Page;
    
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });// Локатор кнопки Checkout
    private ILocator GetCartItem(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });// локатор товара в корзине
    private ILocator CardHeader => Page.Locator("[data-test='title']"); //локатор хедера
    
    public CartPage(IPage page)
    {
        Page = page;
    }
    
    /// Проверяет, что товар видимый 
    public async Task IsProductInCartAsync(string productName)
    {
        await Expect(GetCartItem(productName)).ToBeVisibleAsync();
    }
    
    /// клик по кнопке Checkout
    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
    //проверка названия хедера
    public async Task VerifyTitleCartAsync(string expectedTitle)
    {
        await Expect(CardHeader).ToHaveTextAsync(expectedTitle);
        await Expect(CardHeader).ToBeVisibleAsync();
    }
}