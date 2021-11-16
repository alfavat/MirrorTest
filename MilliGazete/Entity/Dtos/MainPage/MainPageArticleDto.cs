namespace Entity.Dtos
{
    public class MainPageArticleDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public bool UseTitle { get; set; }
        public AuthorDto Author { get; set; }
    }
}
