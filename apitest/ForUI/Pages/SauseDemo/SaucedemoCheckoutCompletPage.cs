using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }
    
    public ILocator GetCompleteHeader()
    {
        return Page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" });
    }
}