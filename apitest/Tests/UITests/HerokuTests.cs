namespace apitest.UITests;
using FluentAssertions;

public class HerokuTests:BaseTest
{
    [Test]
    public async Task CheckBoxtest()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
            var first = Page.Locator("input[type='checkbox']").Nth(0);
            await first.ClickAsync();
            (await first.IsCheckedAsync()).Should().BeTrue();

    }
}