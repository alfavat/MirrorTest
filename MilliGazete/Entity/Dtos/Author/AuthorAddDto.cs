namespace Entity.Dtos
{
    public class AuthorAddDto
    {
        public int? PhotoFileId { get; set; }
        public string NameSurename { get; set; }
        public string HtmlBiography { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string SeoKeywords { get; set; }
        public int? FeaturedImageFileId { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Web { get; set; }
    }
}
