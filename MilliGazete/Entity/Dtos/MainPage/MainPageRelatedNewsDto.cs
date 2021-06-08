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
        public string Url { get; set; }
        public string Thumbnail { get; set; }
    }
}
