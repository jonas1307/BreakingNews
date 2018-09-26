using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BreakingNews.Application.ViewModels
{
    public class FormUserViewModel
    {
        [Display(Name = "Id:")]
        public string Id { get; set; }

        [Display(Name = "Nome de Usuário:")]
        [EmailAddress(ErrorMessage = "O nome de usuário precisa ser um e-mail válido.")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string UserName { get; set; }

        [Display(Name = "Senha:")]
        public string Password { get; set; }

        [Display(Name = "Confirmação de Senha:")]
        public string PasswordConfirmation { get; set; }

        [Display(Name = "Roles:")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public IList<string> SelectedRoles { get; set; }

        public IEnumerable<SelectListItem> RolesListItems { get; set; }
    }
}
