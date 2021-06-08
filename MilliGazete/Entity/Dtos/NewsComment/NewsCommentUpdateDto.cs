namespace Entity.Dtos
{
    public class NewsCommentUpdateDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? TotalLikeCount { get; set; }
        public bool Approved { get; set; }
    }
}
