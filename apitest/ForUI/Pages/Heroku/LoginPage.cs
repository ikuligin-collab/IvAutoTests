using Microsoft.Playwright;

namespace apitest.ForUI.Pages;

public class LoginPage
{
    private readonly IPage Page;
    private ILocator UserNameTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
    private ILocator PassTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
    private ILocator ErrorMessageLabel => Page.Locator("//div[@id='flash]");
    
    public LoginPage(IPage page)
    {
      Page = page;
    }

    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");
    }
    
    public  async Task LoginUserAsync(string username, string password)
    {
        await UserNameTextBox.FillAsync(username);
        await PassTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
    }
    
    public async Task<string> GetTextFromErrorMessageAsync()
    {
        return await ErrorMessageLabel.TextContentAsync();
    }
}