using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class SaucedemoLoginPage
{
    private readonly IPage Page;
    private ILocator UserNameTextBox => Page.GetByPlaceholder("Username");
    private ILocator PassTextBox => Page.GetByPlaceholder("Password");
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });


    public SaucedemoLoginPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://www.saucedemo.com");
    }

    public async Task LoginUserAsync(string username, string password)
    {
        await UserNameTextBox.FillAsync(username);
        await PassTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
    }

}
