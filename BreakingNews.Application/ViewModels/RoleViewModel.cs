using System.ComponentModel.DataAnnotations;

namespace BreakingNews.Application.ViewModels
{
    public class RoleViewModel
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Nome:")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string Name { get; set; }
    }
}