using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleTag = new HashSet<ArticleTag>();
        }

        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public int ReadCount { get; set; }
        public bool Approved { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public string ShortDescription { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<ArticleTag> ArticleTag { get; set; }
    }
}
