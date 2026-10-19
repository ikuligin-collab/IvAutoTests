using Microsoft.Playwright;

namespace apitest.ForUI.Pages;

public class NastedFramePage
{
    private readonly IPage Page;

    private ILocator LeftFrame => Page.FrameLocator("frame[name='frame-top']")
        .FrameLocator("frame[name='frame-left']").Locator("body");

    private ILocator BottomFrame => Page.FrameLocator("frame[name='frame-bottom']")
        .Locator("body");
    public NastedFramePage(IPage page)
    {
        Page = page;
    }
    public async Task<string> GetTextFromLeftFrameAsync()
    {
        return await LeftFrame.InnerTextAsync();
    }
    public async Task<string> GetTextFromBottomFrameAsync()
    {
        return await BottomFrame.InnerTextAsync();
    }
}