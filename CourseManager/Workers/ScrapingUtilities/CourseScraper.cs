using AngleSharp;
using AngleSharp.Dom;
using Data.Entities;
using Data.Migrations;
using Repositories.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Scrapers.ScrapingUtilities;

public static class CourseScraper
{
    public async static Task GetFaculties(IConfiguration configuration, IFacultyRepository facultyRepository)
    {
        // Get a new cookie
        var cookie = await CookieManager.GetCookie(configuration);
        
        // create new http client for the request to pass in headers
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", cookie.Value);
        
        var response = await client.GetAsync(configuration["Scraper:BuilderUrl"]);
        var html = await response.Content.ReadAsStringAsync();
        
        if (string.IsNullOrWhiteSpace(html))
        {
            throw new Exception("No html was returned from the request");
        }
        
        // parse the HTML string to DOM nodes for AngleSharp
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(req => req.Content(html));

        if (document == null)
        {
            throw new Exception("No document was returned from the request");
        }

        var faculties = new List<Faculty>();
        var options = document.QuerySelectorAll("select#Subject > option");

        foreach (var option in options)
        {
            var abbreviation = option.GetAttribute("value");
            var name = option.TextContent.Trim();

            // Skip the first option as it is just a placeholder
            if (!string.IsNullOrWhiteSpace(abbreviation))
            {
                faculties.Add(new Faculty(name, abbreviation));
            }
        }

        await facultyRepository.InsertRangeAsync(faculties);
    }
    
    public static void Scrape()
    {
    } 
}