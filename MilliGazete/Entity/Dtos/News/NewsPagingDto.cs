using System;

namespace Entity.Dtos
{
    public class NewsPagingDto : PagingDto
    {
        public bool? Active { get; set; }
        public bool? IsDraft { get; set; }
        public bool? Approved { get; set; }
        public DateTime? FromPublishedAt { get; set; }
        public DateTime? ToPublishedAt { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public int? NewsAgencyEntityId { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
        public int? AuthorId { get; set; }
        public int? UserId { get; set; }
    }
}
