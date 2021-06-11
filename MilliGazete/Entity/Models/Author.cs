using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Author
    {
        public Author()
        {
            Articles = new HashSet<Article>();
            News = new HashSet<News>();
        }

        public int Id { get; set; }
        public int? PhotoFileId { get; set; }
        public string NameSurename { get; set; }
        public string HtmlBiography { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string SeoKeywords { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public int? FeaturedImageFileId { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Web { get; set; }

        public virtual File FeaturedImageFile { get; set; }
        public virtual File PhotoFile { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
