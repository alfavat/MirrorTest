using System;

namespace Entity.Dtos
{
    public class NewsCommentPagingDto : PagingDto
    {
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
        public bool? Approved { get; set; }
        public int? NewsId { get; set; }
        public int? UserId { get; set; }
    }
}
