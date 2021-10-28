namespace Entity.Dtos
{
    public class FlashNewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public string AuthorNameSurename { get; set; }
        public bool UseTitle { get; set; }
    }
}
