using System.Collections.Generic;
using System.Threading.Tasks;
using BreakingNews.Domain.Entities;

namespace BreakingNews.Application.Interfaces
{
    public interface INewsAppService : IAppServiceBase<News>
    {
        Task<News> GetByFriendlyName(string friendlyName);

        Task<IEnumerable<News>> GetPublicNews();

        Task<IEnumerable<News>> GetNewsByWebService();
    }
}