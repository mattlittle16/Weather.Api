using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MySql;

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    {

    }

    public DbSet<Log> Log { get; set; }
    public DbSet<LogType> LogType { get; set; }
}