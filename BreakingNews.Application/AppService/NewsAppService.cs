using System.Collections.Generic;
using System.Threading.Tasks;
using BreakingNews.Application.Interfaces;
using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Services;

namespace BreakingNews.Application.AppService
{
    public class NewsAppService : AppServiceBase<News>, INewsAppService
    {
        private readonly INewsService _newsService;

        public NewsAppService(IServiceBase<News> serviceBase, INewsService newsService) : base(serviceBase)
        {
            _newsService = newsService;
        }

        public async Task<News> GetByFriendlyName(string friendlyName)
        {
            return await _newsService.QuerySingle(qry => qry.FriendlyUrl == friendlyName);
        }

        public async Task<IEnumerable<News>> GetPublicNews()
        {
            return await _newsService.QueryMultiple(qry => qry.IsPublished);
        }
    }
}
