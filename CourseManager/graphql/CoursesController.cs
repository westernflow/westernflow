using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace graphql.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController(IDbContextFactory<CourseManagerDbContext> dbContextFactory) : ControllerBase
{
    [HttpGet]
    public IActionResult GetIndexedCoursesAndProfessors()
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var courses = dbContext.Courses
           .Select(c => new
            {
                id = c.Id,
                code = (c.Faculty.Abbreviation + " " + c.Number).ToLower(),
                name = c.Name,
                profs = new string [] {},
                rating_count = 0
            })
            .ToList();

        var professors = dbContext.Professors
            .Select(p => new
            {
                id = p.Id,
                name = p.Name,
                courses = new List<object>(),
                code = p.UwoId,
                rating_count = 0
            }).ToList();

        var result = new
        {
            courses,
            profs = professors,
        };

        return Ok(result);
    }
}