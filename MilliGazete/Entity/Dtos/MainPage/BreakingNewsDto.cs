namespace Entity.Dtos
{
    public class BreakingNewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public bool UseTitle { get; set; }
    }
}
