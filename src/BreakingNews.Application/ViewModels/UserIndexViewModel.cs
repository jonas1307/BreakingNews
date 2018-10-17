using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Application.ViewModels
{
    public class UserIndexViewModel
    {
        public IEnumerable<IdentityUser> Users { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
