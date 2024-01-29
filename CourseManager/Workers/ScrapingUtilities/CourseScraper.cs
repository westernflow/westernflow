using System.Diagnostics;
using AngleSharp.Dom;
using Data;
using Data.Entities;
using Data.Entities.EnumTables;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Scrapers.ScrapingUtilities;
using AngleSharp;
using AngleSharp.Io;

public static class CourseScraper
{
    public static List<Course> GetCoursesFromSubjectPage(IDocument document)
    {
        var courses = new List<Course>();
       
        // get all divs with class="panel-default" and splice away the first 2 elements
        var courseDivs = document.QuerySelectorAll("div.panel-default");
        foreach (var courseDiv in courseDivs)
        {
                    
        }
        
        return courses;
    }
    
    public async static Task<List<Course>> GetCoursesFromDocument(IDocument document)
    {
        var courses = new List<Course>();

        // Get the one and only tbody present on the page, representing the table of subjects
        var tableBodies = document.QuerySelectorAll("tbody");
        // assert only one tbody is in the document
        if (tableBodies.Count() != 1) throw new Exception("There should only be one tbody in the document");
        
        var tableBody = tableBodies.Single();
        
        foreach (var row in tableBody.QuerySelectorAll("tr").Take(2))
        {
            var columns = row.QuerySelectorAll("td");
            // assert that there are 3 columns in the row
            if (columns.Count() != 3) throw new Exception("There should be 3 columns in the row");
            var linkElement = columns[0].QuerySelector("a");
            if (linkElement == null)
            {
                throw new InvalidOperationException("The link element was not found in the first column.");
            }
            var subjectPageRelativeUrl = linkElement.GetAttribute("href");
            if (subjectPageRelativeUrl == null) throw new Exception("Subject page url is null");
            var subjectPageUrl = "https://www.westerncalendar.uwo.ca/" + subjectPageRelativeUrl;
            var subjectPageContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            if (subjectPageContext == null) throw new Exception("Subject page context is null");
            var subjectPage = await subjectPageContext.OpenAsync(subjectPageUrl);
            
            courses.AddRange(GetCoursesFromSubjectPage(subjectPage));
        }
        
        
        return courses;
    }
    
    public async static Task Scrape()
    {
      const string url = "https://www.westerncalendar.uwo.ca/Courses.cfm?SelectedCalendar=Live&ArchiveID=";
      
      var config = Configuration.Default.WithDefaultLoader();
      if (config == null) throw new Exception("Configuration is null");

      // Create a browsing context
      var context = BrowsingContext.New(config);
      if (context == null) throw new Exception("Context is null");

      // Load the webpage
      var document = await context.OpenAsync(url);
      if (document == null) throw new Exception("Document is null");

      // Parse the HTML to get back Course entities
      var courses = await GetCoursesFromDocument(document);
    } 
}