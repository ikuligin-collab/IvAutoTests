using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;

    private ILocator Header => Page.GetByRole(AriaRole.Heading);
    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }
    
    public async Task<string> GetHeaderTextAsync() // метод возвращает текст заголовка
    {
        return await Header.TextContentAsync() ?? string.Empty;
    }
}