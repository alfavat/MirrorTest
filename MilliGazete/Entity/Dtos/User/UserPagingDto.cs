using System;

namespace Entity.Dtos
{
    public class UserPagingDto : PagingDto
    {
        public bool? IsEmployee { get; set; }
        public bool? Active { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}
