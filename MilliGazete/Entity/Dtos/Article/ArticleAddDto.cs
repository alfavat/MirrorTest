﻿namespace Entity.Dtos
{
    public class ArticleAddDto
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public int ReadCount { get; set; }
        public bool Approved { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
    }
}
