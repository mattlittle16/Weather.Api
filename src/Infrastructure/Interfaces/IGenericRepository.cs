using Core.Entities;

namespace Infrastructure.Interfaces;

public interface IGenericRepository <TEntity> where TEntity : Base
{
    IQueryable<TEntity> GetAll();

    Task<TEntity> GetById(Guid id);

    Task Create(TEntity entity);

    Task Update(Guid id, TEntity entity);

    Task Delete(Guid id);
}