using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BreakingNews.Application.Interfaces;
using BreakingNews.Domain.Interfaces.Services;

namespace BreakingNews.Application.AppService
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            _serviceBase.Add(obj);
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _serviceBase.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _serviceBase.GetById(id);
        }

        public void Dispose()
        {
            _serviceBase = null;
        }
    }
}