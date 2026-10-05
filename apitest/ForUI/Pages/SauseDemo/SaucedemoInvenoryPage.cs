using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class InventoryPage
{
    private readonly IPage Page;

    private ILocator CartButton =>Page.Locator("[data-test='shopping-cart-link']"); //локатор для корзины

    public InventoryPage(IPage page)
    {
        Page = page;
    }

    //Нажатие кнопки у товара
    public async Task AddToCartByNameAsync(string productName)
    {
        await Page.Locator(".inventory_item")
            .Filter(new() { HasText = productName })
            .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
            .ClickAsync();
    }
    
    //Поиск товара на странице
    public ILocator GetProductContainerByName(string productName)
    {
        return Page.Locator(".inventory_item")
            .Filter(new() { HasText = productName });
    }
    
    //клик по корзине
    public async Task OpenCartAsync()
    {
        await CartButton.ClickAsync();
    }
}