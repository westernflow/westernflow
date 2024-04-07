using Microsoft.Extensions.Caching.Memory;
using PuppeteerSharp;

namespace Scrapers.Utilities;

public static class DirectoryScraper
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
    
    public record DirectoryProfessor
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string UwoId { get; set; } = String.Empty;
        public string Departments { get; set; } = String.Empty;
    }
    
    public enum SearchOption
    {
        StartsWith,
        Contains
    }

    public static async Task<List<DirectoryProfessor>> GetProfessorsInDirectory(string firstName, string lastName, SearchOption option)
    {
        var cacheKey = $"GetProfessorsInDirectory-{firstName}-{lastName}-{option}";
        if (Cache.TryGetValue(cacheKey, out List<DirectoryProfessor> cachedResult))
        {
            return cachedResult;
        }
        
        var configuration = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();
        
        var options = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = configuration["Scraper:ChromeExecutablePath"],
        };
        
        var browser = await Puppeteer.LaunchAsync(options);
        
        var page = await browser.NewPageAsync();

        await page.GoToAsync("https://www.uwo.ca/directory.html");

        // Use a more specific selector to fill the input fields
        await page.TypeAsync(".TabbedPanelsContent-wide #first_name", firstName);
        await page.TypeAsync(".TabbedPanelsContent-wide #last_name", lastName);

        switch (option)
        {
            case SearchOption.StartsWith:
                await page.ClickAsync(".TabbedPanelsContent-wide input[name=info][value='Starts with']");
                break;
            case SearchOption.Contains:
                await page.ClickAsync(".TabbedPanelsContent-wide input[name=info][value='Contains']");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }

        await page.WaitForNavigationAsync(new NavigationOptions
        {
            WaitUntil = new[] {WaitUntilNavigation.Networkidle0},
            Timeout = 10000
        });
        
        // select table with id id="people_search_results"
        var table = await page.QuerySelectorAsync("#people_search_results");
        if (table == null)
        {
            Console.WriteLine("Error finding the table.");
            await browser.CloseAsync();
            return new List<DirectoryProfessor>();
        }
        
        // get all rows in the table
        var rows = await table.QuerySelectorAllAsync("tr");
        
        var professors = new List<DirectoryProfessor>();
        
        foreach (var row in rows.Skip(1))
        {
            var cells = await row.QuerySelectorAllAsync("td");
            var name = await cells[0].EvaluateFunctionAsync<string>("node => node.textContent");
            if (name.Split(',')[0].ToLower().Trim() != lastName.ToLower().Trim())
            {
                continue;
            }
            
            var emailLink = await cells[1].QuerySelectorAsync("a");
            if (emailLink == null)
            {
                continue;
            }
            var email = await emailLink.EvaluateFunctionAsync<string>("node => node.textContent");

            var departments = await cells[2].EvaluateFunctionAsync<string>("node => node.textContent");
            
            professors.Add(new DirectoryProfessor
            {
                Name = name.Trim(),
                Email = email.Trim(),
                UwoId = email.Split('@')[0].Trim(),
                Departments = departments.Trim()
            });

        }

        Cache.Set(cacheKey, professors, TimeSpan.FromMinutes(1000));
        
        await browser.CloseAsync();

        return professors;
    }
}