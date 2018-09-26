using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BreakingNews.Domain.Interfaces.Repositories;
using BreakingNews.Domain.Interfaces.Services;

namespace BreakingNews.Domain.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        protected IRepositoryBase<TEntity> Repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            Repository = repository;
        }

        public async Task AddAsync(TEntity obj)
        {
            await Repository.AddAsync(obj);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await Repository.Update(obj);
        }

        public async Task RemoveAsync(TEntity obj)
        {
            await Repository.Remove(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Repository.GetById(id);
        }

        public async Task<IEnumerable<TEntity>> QueryMultiple(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.QueryMultiple(predicate);
        }

        public async Task<TEntity> QuerySingle(Expression<Func<TEntity, bool>> predicate)
        {
            return await Repository.QuerySingle(predicate);
        }

        public void Dispose()
        {
            Repository = null;
        }
    }
}
