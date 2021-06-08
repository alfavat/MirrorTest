using System;

namespace Entity.Dtos
{
    public class NewsAgencyPagingDto : PagingDto
    {
        public DateTime? FromPublishedAt { get; set; }
        public DateTime? ToPublishedAt { get; set; }
        public int? NewsAgencyId { get; set; }
    }
}
