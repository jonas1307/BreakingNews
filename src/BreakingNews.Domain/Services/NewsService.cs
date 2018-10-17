using System.Threading.Tasks;
using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Repositories;
using BreakingNews.Domain.Interfaces.Services;

namespace BreakingNews.Domain.Services
{
    public class NewsService : ServiceBase<News>, INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(IRepositoryBase<News> repository, INewsRepository newsRepository) : base(repository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<News> GetByFriendlyName(string friendlyName)
        {
            return await _newsRepository.QuerySingle(qry => qry.FriendlyUrl == friendlyName);
        }
    }
}
