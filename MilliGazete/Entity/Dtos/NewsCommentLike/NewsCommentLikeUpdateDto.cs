namespace Entity.Dtos
{
    public class NewsCommentLikeUpdateDto
    {
        public int Id { get; set; }
        public int NewsCommentId { get; set; }
        public int UserId { get; set; }
        public bool? IsLike { get; set; }
    }
}
