using System.Collections.Generic;
using System.Threading.Tasks;

namespace BreakingNews.Application.Interfaces
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);

        Task<TEntity> Update(int id, TEntity obj);

        Task Remove(int id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);
    }
}