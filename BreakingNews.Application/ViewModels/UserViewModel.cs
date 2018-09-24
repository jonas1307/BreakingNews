using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BreakingNews.Application.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public IList<string> SelectedRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
