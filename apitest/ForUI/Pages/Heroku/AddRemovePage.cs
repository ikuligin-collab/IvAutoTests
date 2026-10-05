using Microsoft.Playwright;

namespace apitest.ForUI.Pages;

public class AddRemovePage
{
    private readonly IPage Page;
    private ILocator Button(string buttonName) => Page.GetByRole(AriaRole.Button, new() { Name = $"{buttonName}" });
    
    public AddRemovePage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAddRemovePage()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");
    }

    public async Task CheckPageOpenAsync()
    {
        // Проверка title
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");

        // Проверка URL
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/add_remove_elements/");
    }

    public async Task ClickButtonByNameAsync(string buttonName)
    {
        await Button(buttonName).ClickAsync();
    }

    public async Task CheckNumberOfButtonAsync(string buttonName, int count)
    {
        await Assertions.Expect(Button(buttonName)).ToHaveCountAsync(count);
    }
    
    public async Task ClickButtonByNameAndIndexAsync(string buttonName, int number)
    {
        await Button(buttonName).Nth(number-1).ClickAsync();
    }
}