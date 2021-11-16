namespace Entity.Dtos
{
    public class MainPageRelatedNewsDto
    {
        public int RelatedNewsId { get; set; }
        public virtual MainPageRelatedNewsDetailsDto RelatedNews { get; set; }
    }
    public class MainPageRelatedNewsDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public string Thumbnail { get; set; }
        public int? HistoryNo { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public bool UseTitle { get; set; }
    }
}
