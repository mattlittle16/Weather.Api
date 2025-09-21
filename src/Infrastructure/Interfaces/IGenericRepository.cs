using System.Linq.Expressions;
using Core.Entities;

namespace Infrastructure.Interfaces;

public interface IGenericRepository <T> where T : Base
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);

    Task<T> GetById(Guid id);

    Task Create(T entity);

    Task Update(Guid id, T entity);

    Task Delete(Guid id);
}