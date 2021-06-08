using System;

namespace Entity.Dtos
{
    public class NewsCommentLikeDto
    {
        public int Id { get; set; }
        public int NewsCommentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsLike { get; set; }
        public virtual NewsCommentDto NewsComment { get; set; }
        public virtual UserViewDto User { get; set; }
    }
}
