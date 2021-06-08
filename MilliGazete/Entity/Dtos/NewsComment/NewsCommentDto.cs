using System;

namespace Entity.Dtos
{
    public class NewsCommentDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? TotalLikeCount { get; set; }
        public bool Approved { get; set; }
        public UserViewDto User { get; set; }
        public CommentNewsDetailsDto News { get; set; }
    }
}
