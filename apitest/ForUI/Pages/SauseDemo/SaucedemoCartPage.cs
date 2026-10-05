using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CartPage
{
    private readonly IPage Page;

    // Локатор кнопки Checkout
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });

    public CartPage(IPage page)
    {
        Page = page;
    }
    
    /// Возвращает локатор контейнера товара в корзине по его названию
    public ILocator GetCartItemByName(string productName)
    {
        return Page.Locator(".cart_item")
            .Filter(new() { HasText = productName });
    }
    
    /// клик по кнопке Checkout
    public async Task ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}