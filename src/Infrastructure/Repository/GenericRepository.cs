using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class GenericRepository <TEntity> : IGenericRepository<TEntity> where TEntity : Base
{
    private readonly WeatherDbContext _dbContext;

    public GenericRepository(WeatherDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await _dbContext.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task Update(Guid id, TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        throw new NotImplementedException();
    }
}