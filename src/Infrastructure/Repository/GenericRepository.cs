using System.Linq.Expressions;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.MySql;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class GenericRepository <T> : IGenericRepository<T> where T : Base
{
    private readonly WeatherDbContext _dbContext;

    public GenericRepository(WeatherDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity is null");

        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task Update(Guid id, T entity)
    {
        if (entity == null)
            throw new ArgumentNullException("entity is null");

        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {        
        var entity = await GetById(id);

        if (entity == null)
            throw new ArgumentNullException("entity is null");

        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
    {
        if (predicate != null)
        {
            return _dbContext.Set<T>().Where(predicate);
        }
        else 
        {
            return _dbContext.Set<T>().AsQueryable<T>();
        }
    }
}