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
        var options = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = configuration["Scraper:ChromeExecutablePath"]
        };
            
         // setup 
         var browser = await Puppeteer.LaunchAsync(options);
         var page = await browser.NewPageAsync();
         
         // Navigate to Scraper:LoginUrl
         await page.GoToAsync(configuration["Scraper:LoginUrl"]);

         // Fill the form
         await page.TypeAsync("input[name='txtUsername']", "YourUsername"); // Replace with actual username
         await page.TypeAsync("input[name='txtPassword']", "YourPassword"); // Replace with actual password

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