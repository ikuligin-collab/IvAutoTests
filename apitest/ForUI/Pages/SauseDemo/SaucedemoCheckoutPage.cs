using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutPage
{
    private readonly IPage Page;
    
    private ILocator FirstNameInput => Page.GetByPlaceholder("First Name");
    private ILocator LastNameInput => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeInput => Page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public CheckoutPage(IPage page)
    {
        Page = page;
    }
    
    public async Task FillInformationAndContinueAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await PostalCodeInput.FillAsync(postalCode);
        await ContinueButton.ClickAsync();
    }
}