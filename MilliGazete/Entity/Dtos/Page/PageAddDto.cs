namespace Entity.Dtos
{
    public class PageAddDto
    {
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public int? FeaturedImageFileId { get; set; }

    }
}
