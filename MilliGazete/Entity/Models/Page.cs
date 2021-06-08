using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Page
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public int? FeaturedImageFileId { get; set; }

        public virtual File FeaturedImageFile { get; set; }
    }
}
