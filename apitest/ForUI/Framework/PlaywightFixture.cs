using Microsoft.Playwright;

namespace apitest.ForUI.Framework;

public class PlaywightFixture : IAsyncDisposable
{
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await  Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 2000,
            Args = new[] { "--start-maximized" }
            
        });
    }

    public async ValueTask DisposeAsync()
    {
        if (Browser != null)
        {
            await Browser.CloseAsync();
        }
        Playwright?.Dispose();
    }
}