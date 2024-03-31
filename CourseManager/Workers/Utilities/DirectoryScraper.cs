using Microsoft.Extensions.Caching.Memory;
using PuppeteerSharp;

namespace Scrapers.Utilities;

public static class DirectoryScraper
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
    
    public record DirectoryProfessor
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
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
            Console.WriteLine("Cached result found! Returning cached result.");
            return cachedResult;
        }
        
        var configuration = (ServiceProvider ?? throw new InvalidOperationException()).GetRequiredService<IConfiguration>();
        
        var options = new LaunchOptions
        {
            Headless = false,
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

        var response = await page.WaitForNavigationAsync(new NavigationOptions
        {
            WaitUntil = new[] {WaitUntilNavigation.Networkidle0},
            Timeout = 10000
        });
        
        // get html of new page
        var html = await page.GetContentAsync();
        
        // select table with id id="people_search_results"
        var table = await page.QuerySelectorAsync("#people_search_results");
        
        // get all rows in the table
        var rows = await table.QuerySelectorAllAsync("tr");
        
        var professors = new List<DirectoryProfessor>();
        
        foreach (var row in rows.Skip(1))
        {
            var cells = await row.QuerySelectorAllAsync("td");
            var name = await cells[0].EvaluateFunctionAsync<string>("node => node.textContent");
            var emailLink = await cells[1].QuerySelectorAsync("a");
            var email = await emailLink.EvaluateFunctionAsync<string>("node => node.textContent");
            var departments = await cells[2].EvaluateFunctionAsync<string>("node => node.textContent");
            
            // get first name and last name split by comma and trim
            var names = name.Split(",");
            var first = names[1].Trim();
            var last = names[0].Trim();
            
            // get uwo id by splitting email by @ and taking the first part
            var uwoId = email.Split("@")[0].Trim();
            
            // clean name, email departments whitespace and insert into professor lists
            professors.Add(new DirectoryProfessor
            {
                FirstName = first,
                LastName = last,
                UwoId = uwoId,
                Email = email.Trim(),
                Departments = departments.Trim()
            });

            Console.WriteLine($"Puppeteer scraped professor: {first} - {last} - {uwoId} - {email} - {departments}");
        }

        Cache.Set(cacheKey, professors, TimeSpan.FromMinutes(1000));
        
        await browser.CloseAsync();

        return professors;
    }
}