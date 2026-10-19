using Microsoft.Playwright;

namespace apitest.ForUI.Pages;

public class FramesPage
{
    private readonly IPage Page;
    private ILocator NestedFramesLink => Page.GetByRole(AriaRole.Link, new(){Name = "Nested Frames"});
    public FramesPage(IPage page)
        {
        Page = page;
        }

    public async Task OpenFramesPageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/frames");
    }

    public async Task ClickNastedFramesLink()
    {
        await NestedFramesLink.ClickAsync();
    }
}