using Core.Entities;
using Core.Enums;
using Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Api.Startup;

internal class DbInitializer
{
    internal static void Initialize(WeatherDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));      
        //dbContext.Database.EnsureCreated();

        //apply any migrations
        dbContext.Database.Migrate();
    
        if (dbContext.LogType.Any()) return;

        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.General });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.Request });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.Response });
        dbContext.LogType.Add(new LogType { Type = LogTypeEnum.Error });

        dbContext.SaveChanges();
    }
}