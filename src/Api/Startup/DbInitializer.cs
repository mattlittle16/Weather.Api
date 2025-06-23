using Core.Entities;
using Core.Enums;
using Infrastructure.MySql;

namespace Api.Startup;

internal class DbInitializer
{
    internal static void Initialize(WeatherDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));      
        //dbContext.Database.EnsureCreated();

        //apply any migrations
        //dbContext.Database.Migrate();
    
        if (dbContext.LogType.Any()) return;

        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.General, Description = "general" });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.Request, Description = "request" });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.Response, Description = "response" });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.Error, Description = "error" });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.System, Description  = "system" });

        dbContext.SaveChanges();
    }
}