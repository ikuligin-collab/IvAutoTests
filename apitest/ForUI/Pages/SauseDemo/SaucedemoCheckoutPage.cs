using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace apitest.ForUI.Pages.SauseDemo;

public class CheckoutPage
{
    private readonly IPage Page;

    private ILocator FirstNameInput => Page.GetByPlaceholder("First Name");
    private ILocator LastNameInput => Page.GetByPlaceholder("Last Name");
    private ILocator PostalCodeInput => Page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
    private ILocator CheckoutTitle => Page.Locator("[data-test='title']");

    public CheckoutPage(IPage page)
    {
        Page = page;
    }

    //Заполнение полей и клик на кнопку
    public async Task FillInformationAndContinueAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await PostalCodeInput.FillAsync(postalCode);
        await ContinueButton.ClickAsync();
    }

    //проверка названия и видимости хедера
    public async Task VerifyTitleCheckoutAsync(string expectedTitle)
    {
        await Expect(CheckoutTitle).ToHaveTextAsync(expectedTitle);
        await Expect(CheckoutTitle).ToBeVisibleAsync();
    }
}