using System;

namespace Entity.Dtos
{
    public class NewsFilePagingDto : PagingDto
    {
        public int NewsFileTypeEntityId { get; set; } = 0;
    }
}
