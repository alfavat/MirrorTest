using System;

namespace Entity.Dtos
{
    public class ContactPagingDto : PagingDto
    {
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}
