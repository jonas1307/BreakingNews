using System;
using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
