using System;
using System.ComponentModel.DataAnnotations;

namespace BreakingNews.Application.ViewModels
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            Id = 0;
            IsPublished = false;
            CreationDate = null;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "URL amigável:")]
        [MaxLength(256, ErrorMessage = "É permitido até {1} caracteres.")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string FriendlyUrl { get; set; }

        [Display(Name = "Título:")]
        [MaxLength(256, ErrorMessage = "É permitido até {1} caracteres.")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string Title { get; set; }

        public string TextContent { get; set; }

        [Display(Name = "Conteúdo:")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string HtmlContent { get; set; }

        [Display(Name = "Autor:")]
        [MaxLength(256, ErrorMessage = "É permitido até {1} caracteres.")]
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public string Author { get; set; }

        [Display(Name = "Tornar público:")]
        public bool IsPublished { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de publicação:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? PublishDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de criação:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Útlima atualização:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
