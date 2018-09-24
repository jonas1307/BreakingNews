using System.Threading.Tasks;
using BreakingNews.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        new Task<User> GetById(int id);

        Task<IdentityUser> GetById(string id);
    }
}