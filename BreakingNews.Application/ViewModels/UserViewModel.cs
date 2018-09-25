using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BreakingNews.Application.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Id:")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string Id { get; set; }

        [Display(Name = "Nome de Usuário:")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string UserName { get; set; }

        [Display(Name = "Roles:")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public IList<string> SelectedRoles { get; set; }

        public IEnumerable<SelectListItem> RolesListItems { get; set; }
    }
}
