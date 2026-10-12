using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutPage2
{
    private readonly IPage Page;
    
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });//локатор кноки финиш
    private ILocator ProductName(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });//локатор карточки продукта
    private ILocator CheckoutPage2Title => Page.Locator("[data-test='title']"); 
    
    public CheckoutPage2(IPage page)
    {
        Page = page;
    }
    //Поиск продуктов на странице
    public async Task IsProductInCheckoutPageAsync(string productName)
    {
        await Expect(ProductName(productName)).ToBeVisibleAsync();
    }
    //нажитие кнопки финиш
    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }
    //проверка названия хедера
    public async Task VerifyTitleCheckoutAsync(string expectedTitle)
    {
        await Expect(CheckoutPage2Title).ToHaveTextAsync(expectedTitle);
        await Expect(CheckoutPage2Title).ToBeVisibleAsync();
    }
}