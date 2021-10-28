namespace Entity.Dtos
{
    public class MostSharedNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public string ImageUrl { get; set; }
        public int ShareCount { get; set; }
        public string AuthorNameSurename { get; set; }
        public bool UseTitle { get; set; }
    }
}
