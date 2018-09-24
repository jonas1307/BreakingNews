using System;
using System.Linq;
using System.Threading.Tasks;
using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Infrastructure.Database.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public new Task<User> GetById(int id)
        {
            throw new InvalidOperationException();
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await Context.Users.FindAsync(id);
        }
    }
}
