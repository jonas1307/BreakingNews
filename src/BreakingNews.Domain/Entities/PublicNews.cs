using System;

namespace BreakingNews.Domain.Entities
{
    public class PublicNews
    {
        public string FriendlyUrl { get; set; }

        public string Title { get; set; }

        public string TextContent { get; set; }

        public string HtmlContent { get; set; }

        public string Author { get; set; }

        public DateTime? PublishDate { get; set; }
    }
}
