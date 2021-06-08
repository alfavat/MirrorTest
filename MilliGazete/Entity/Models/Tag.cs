using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ArticleTag = new HashSet<ArticleTag>();
            NewsTag = new HashSet<NewsTag>();
        }

        public int Id { get; set; }
        public string TagName { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<ArticleTag> ArticleTag { get; set; }
        public virtual ICollection<NewsTag> NewsTag { get; set; }
    }
}
