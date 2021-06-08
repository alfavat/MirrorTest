using System;

namespace Entity.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public int? PhotoFileId { get; set; }
        public string NameSurename { get; set; }
        public string HtmlBiography { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string SeoKeywords { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastArticleTitle { get; set; }
        public string LastArticleUrl { get; set; }
        public DateTime? LastArticleDate { get; set; }
        public int? FeaturedImageFileId { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Web { get; set; }

        public virtual FileDto PhotoFile { get; set; }
        public virtual FileDto FeaturedImageFile { get; set; }
    }
}
