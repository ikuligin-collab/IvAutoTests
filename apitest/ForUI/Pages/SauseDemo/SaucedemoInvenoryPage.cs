using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class InventoryPage
{
    private readonly IPage Page;

    private ILocator CartButton =>Page.Locator("[data-test='shopping-cart-link']"); //локатор для корзины
    private ILocator GetProductContainer(string productName) =>
        Page.Locator(".inventory_item").Filter(new() { HasText = productName }); //локатор всей карточки товара
    private ILocator AddToCartButton(string productName) =>
        GetProductContainer(productName).GetByRole(AriaRole.Button, new() { Name = "Add to cart" });//локатор кнопки "Add to cart" у конкретного товара
    private ILocator HeaderLogo => Page.Locator(".app_logo"); //локатор логотипа

    public InventoryPage(IPage page)
    {
        Page = page;
    }

    //Нажатие кнопки у товара
    public async Task AddToCartByNameAsync(string productName)
    {
        await AddToCartButton(productName).ClickAsync();
    }
    
    //Проверка отображения товара на странице
    public async Task<bool> IsProductVisibleAsync(string productName)
    {
        return await GetProductContainer(productName).IsVisibleAsync();
    }
    
    //клик по корзине
    public async Task OpenCartAsync()
    {
        await CartButton.ClickAsync();
    }
    
    public async Task<string> GetHeaderTitleTextAsync()//возврат текста заголовка
    {
        return await HeaderLogo.TextContentAsync() ?? string.Empty;
    }
}