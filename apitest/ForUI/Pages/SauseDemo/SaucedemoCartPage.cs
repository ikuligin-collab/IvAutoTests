using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CartPage
{
    private readonly IPage Page;

    // Локатор кнопки Checkout
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });
    // локатор товара в корзине
    private ILocator GetCartItem(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });
    
    public CartPage(IPage page)
    {
        Page = page;
    }
    
    /// Проверяет, что найденный элемент видимый
    public async Task<bool> IsProductInCartAsync(string productName)
    {
        return await GetCartItem(productName).IsVisibleAsync();
    }
    
    /// клик по кнопке Checkout
    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}