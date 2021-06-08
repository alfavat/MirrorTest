using System;

namespace Entity.Dtos
{
    public class TagPagingDto : PagingDto
    {
        public bool? Active { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}
