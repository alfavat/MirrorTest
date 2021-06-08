using System;

namespace Entity.Dtos
{
    public class ArticlePagingDto : PagingDto
    {
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
        public bool? Approved { get; set; }
    }
}
