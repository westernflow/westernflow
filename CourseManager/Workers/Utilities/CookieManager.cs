using AngleSharp.Dom;
using PuppeteerSharp;

namespace Scrapers.ScrapingUtilities;

public record Cookie
{
    public string Value { get; set; } = String.Empty;
}

public static class CookieManager
{
    public  static async Task<Cookie> GetCookie(IConfiguration configuration)
    {
        // GetCookie will always succeed even if already logged in and previous cookies are valid since it will
        // still prompt for user and password on the login screen and give you a new cookie
        var options = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = configuration["Scraper:ChromeExecutablePath"],
            Args = new string[]{"--disable-gpu","--no-sandbox"}
        };

         // Initialize the browser
         var browser = await Puppeteer.LaunchAsync(options);
         var page = await browser.NewPageAsync();

         // Open builder page
         await page.GoToAsync(configuration["Scraper:LoginUrl"]);

         // Fill the form
         await page.TypeAsync("input[name='txtUsername']", configuration["Scraper:Username"]);
         await page.TypeAsync("input[name='txtPassword']", configuration["Scraper:Password"]);

         // Submit the form
         await page.ClickAsync("button[type='submit']");

         // Wait for navigation or specific element that indicates successful login
         await page.WaitForNavigationAsync();

         // Get cookies
         var cookies = await page.GetCookiesAsync();

         await browser.CloseAsync();

         return new Cookie()
         {
             Value = string.Join("; ", cookies.Select(c => $"{c.Name}={c.Value}"))
         };
    }
}
