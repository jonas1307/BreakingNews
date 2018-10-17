using System;

namespace BreakingNews.Application.ViewModels
{
    public class PublicNewsViewModel
    {
        public int Id { get; set; }

        public string FriendlyUrl { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
