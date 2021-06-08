namespace Entity.Dtos
{
    public class NewsCommentLikeAddDto
    {
        public int NewsCommentId { get; set; }
        public int UserId { get; set; }
        public bool? IsLike { get; set; }
    }
}
