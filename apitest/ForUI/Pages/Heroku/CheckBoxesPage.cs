using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace apitest.ForUI.Pages;

public class CheckBoxesPage
{
    private readonly IPage page;

    private ILocator Checkbox => page.Locator("input[type='checkbox']");

    public CheckBoxesPage(IPage page)
    {
        this.page = page;
    }

    public async Task OpenCheckboxesPageAsync()
    {
        await page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
    }

    public async Task<bool> GetStateOfCheckboxAsync(int number)
    {
        var state = await Checkbox.Nth(number).IsCheckedAsync();
        return state;
    }


    public async Task UncheckCheckboxAsync(int number)
    {
        await Checkbox.Nth(number).UncheckAsync();
    }


    public async Task CheckCheckboxAsync(int number)
    {
        await Checkbox.Nth(number).CheckAsync();
    }
}