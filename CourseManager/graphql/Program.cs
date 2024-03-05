using Data;
using Microsoft.EntityFrameworkCore;

namespace graphql;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration); 
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        startup.Configure(app);

        app.UseRouting();
        app.UseCors();
        app.MapGraphQL();
        app.MapControllers();
        app.Run();
    }
}