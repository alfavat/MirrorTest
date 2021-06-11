namespace Entity.Dtos
{
    public class NewsCommentAddDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? InitialLikeCount { get; set; }
        public string AnonymousUsername { get; set; }
    }
}
