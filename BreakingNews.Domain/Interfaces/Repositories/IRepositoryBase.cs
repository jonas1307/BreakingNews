using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BreakingNews.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> QueryMultiple(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> QuerySingle(Expression<Func<TEntity, bool>> predicate);

        void Remove(TEntity obj);

        void Update(TEntity obj);
    }
}