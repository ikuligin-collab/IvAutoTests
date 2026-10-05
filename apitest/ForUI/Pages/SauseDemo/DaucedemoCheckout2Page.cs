using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutPage2
{
    private readonly IPage Page;
    
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });

    public CheckoutPage2(IPage page)
    {
        Page = page;
    }
    
    public ILocator GetItemByName(string productName)
    {
        return Page.Locator(".cart_item")
            .Filter(new() { HasText = productName });
    }
    
    public async Task ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
    }
}