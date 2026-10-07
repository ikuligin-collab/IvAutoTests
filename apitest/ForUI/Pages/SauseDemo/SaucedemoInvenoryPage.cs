using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace apitest.ForUI.Pages.SauseDemo;

public class InventoryPage
{
    private readonly IPage Page;

    private ILocator CartButton =>Page.Locator("[data-test='shopping-cart-link']"); //локатор для корзины
    private ILocator GetProductContainer(string productName) =>
        Page.Locator(".inventory_item").Filter(new() { HasText = productName }); //локатор всей карточки товара
    private ILocator AddToCartButton(string productName) =>
        GetProductContainer(productName).GetByRole(AriaRole.Button, new() { Name = "Add to cart" });//локатор кнопки "Add to cart" у конкретного товара
    private ILocator InventoryHeader => Page.Locator("[data-test='title']"); //локатор хедера

    public InventoryPage(IPage page)
    {
        Page = page;
    }

    //Нажатие кнопки "Add to cart" у товара
    public async Task AddToCartByNameAsync(string productName)
    {
        await AddToCartButton(productName).ClickAsync();
    }
    
    //Проверка отображения товара на странице
    public async Task VerifyProductIsVisibleAsync(string productName)
    {
        await Expect(GetProductContainer(productName)).ToBeVisibleAsync();
    }
    
    //клик по корзине
    public async Task OpenCartAsync()
    {
        await CartButton.ClickAsync();
    }
    
    //проверка названия хедера
    public async Task VerifyTitleInventoryAsync(string expectedTitle)
    {
        await Expect(InventoryHeader).ToHaveTextAsync(expectedTitle);
        await Expect(InventoryHeader).ToBeVisibleAsync();
    }
}