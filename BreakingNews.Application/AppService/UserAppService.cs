using System.Threading.Tasks;
using BreakingNews.Application.Interfaces;
using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Application.AppService
{
    public class UserAppService : AppServiceBase<User>, IUserAppService
    {
        private readonly IUserService _userService;


        public UserAppService(IServiceBase<User> serviceBase, IUserService userService) : base(serviceBase)
        {
            _userService = userService;
        }

        public async Task<IdentityUser> GetByUsername(string userName)
        {
            return await _userService.QuerySingle(qry => qry.Id == userName);
        }
    }
}
