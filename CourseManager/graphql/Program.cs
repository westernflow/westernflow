using Data;
using Microsoft.EntityFrameworkCore;

namespace graphql;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        var startup = new Startup(builder.Configuration); 
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        startup.Configure(app);

        app.MapGraphQL();
        app.MapControllers();
        app.Run();
    }
}