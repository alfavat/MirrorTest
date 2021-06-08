using System;

namespace Entity.Dtos
{
    public class PagePagingDto : PagingDto
    {
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}
