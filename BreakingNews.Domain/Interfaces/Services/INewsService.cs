using System.Collections.Generic;
using System.Threading.Tasks;
using BreakingNews.Domain.Entities;

namespace BreakingNews.Domain.Interfaces.Services
{
    public interface INewsService : IServiceBase<News>
    {
        Task<News> GetByFriendlyName(string friendlyName);
    }
}