namespace Entity.Dtos
{
    public class MostViewedNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public string ImageUrl { get; set; }
        public int ViewCount { get; set; }
        public bool UseTitle { get; set; }
    }
}
