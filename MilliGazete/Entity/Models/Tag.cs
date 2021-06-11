using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ArticleTags = new HashSet<ArticleTag>();
            NewsTags = new HashSet<NewsTag>();
        }

        public int Id { get; set; }
        public string TagName { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<NewsTag> NewsTags { get; set; }
    }
}
