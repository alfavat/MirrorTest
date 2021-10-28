using System;

namespace Entity.Dtos
{
    public class PageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsStatic { get; set; }
        public int? FeaturedImageFileId { get; set; }

        public virtual FileDto FeaturedImageFile { get; set; }
    }
}
