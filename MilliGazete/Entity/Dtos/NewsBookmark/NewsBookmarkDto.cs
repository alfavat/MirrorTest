namespace Entity.Dtos
{
    public class NewsBookmarkDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public int UserId { get; set; }
    }
}
