using System;

namespace BreakingNews.Domain.Entities
{
    public class News
    {
        public int Id { get; set; }

        public string FriendlyUrl { get; set; }

        public string Title { get; set; }

        public string TextContent { get; set; }

        public string HtmlContent { get; set; }

        public string Author { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }
}
