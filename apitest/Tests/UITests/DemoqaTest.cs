using static Microsoft.Playwright.Assertions;
using Microsoft.Playwright;


namespace apitest.UITests;

public class DemoqaTest : BaseTest
{
    [Test]
    public async Task CheckBoxtest()
    {
        await Page.GotoAsync("https://demoqa.com/select-menu");
        await Assertions.Expect(Page).ToHaveURLAsync("https://demoqa.com/select-menu");
        var selectOneDropdown = Page.Locator("#selectOne");
        await selectOneDropdown.ClickAsync();
        await Page.Locator("[id^='react-select-3-option']", new() { HasText = "Prof." }).ClickAsync();
        await Expect(Page.Locator("#selectOne")).ToContainTextAsync("Prof.");
    }
}