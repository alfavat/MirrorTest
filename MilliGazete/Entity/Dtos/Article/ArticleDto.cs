using System;

namespace Entity.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public int ReadCount { get; set; }
        public bool Approved { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishDate { get; set; }
        public AuthorDto Author { get; set; }
        public string ImageUrl { get; set; }
    }
}
