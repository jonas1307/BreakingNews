using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BreakingNews.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);

        Task UpdateAsync(TEntity obj);

        Task RemoveAsync(TEntity obj);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> QueryMultiple(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> QuerySingle(Expression<Func<TEntity, bool>> predicate);
    }
}