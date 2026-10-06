using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutPage2
{
    private readonly IPage Page;
    
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });
    
    private ILocator ProductName(string productName) =>
        Page.Locator(".cart_item").Filter(new() { HasText = productName });
    
    public CheckoutPage2(IPage page)
    {
        Page = page;
    }
    
    public async Task<bool> IsProductInChecoutPageAsync(string productName)
    {
        return await  ProductName (productName).IsVisibleAsync();
    }
    
    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }
}