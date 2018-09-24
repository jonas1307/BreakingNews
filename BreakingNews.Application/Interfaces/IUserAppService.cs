using System.Threading.Tasks;
using BreakingNews.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Application.Interfaces
{
    public interface IUserAppService : IAppServiceBase<User>
    {
        Task<IdentityUser> GetByUsername(string userName);
    }
}