using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BreakingNews.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BreakingNews.Infrastructure.Database.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly BreakingNewsContext Context = new BreakingNewsContext();

        public async Task AddAsync(TEntity obj)
        {
            await Task.Run(() => Context.Set<TEntity>().Add(obj));
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Task.Run(() => Context.Set<TEntity>());
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> QueryMultiple(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => Context.Set<TEntity>().Where(predicate));
        }

        public async Task<TEntity> QuerySingle(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task Remove(TEntity obj)
        {
            await Task.Run(() => Context.Set<TEntity>().Remove(obj));
            await Context.SaveChangesAsync();
        }

        public async Task Update(TEntity obj)
        {
            await Task.Run(() => Context.Entry(obj).State = EntityState.Modified);
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
