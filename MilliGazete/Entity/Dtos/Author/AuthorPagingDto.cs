using System;

namespace Entity.Dtos
{
    public class AuthorPagingDto : PagingDto
    {
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}
