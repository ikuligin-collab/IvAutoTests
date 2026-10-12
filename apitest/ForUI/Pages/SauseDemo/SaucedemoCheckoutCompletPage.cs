using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;

    private ILocator HeadeCompletePager => Page.GetByRole(AriaRole.Heading);

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    //проверка названия хедера
    public async Task VerifyTitleCheckoutAsync(string expectedTitle)
    {
        await Expect(HeadeCompletePager).ToHaveTextAsync(expectedTitle);
        await Expect(HeadeCompletePager).ToBeVisibleAsync();
    }
}