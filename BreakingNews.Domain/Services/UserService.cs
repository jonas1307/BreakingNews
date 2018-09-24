using System.Threading.Tasks;
using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Repositories;
using BreakingNews.Domain.Interfaces.Services;

namespace BreakingNews.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IRepositoryBase<User> repository)
            : base(repository)
        { }
    }
}
