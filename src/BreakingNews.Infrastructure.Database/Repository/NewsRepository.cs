using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Repositories;

namespace BreakingNews.Infrastructure.Database.Repository
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    { }
}
