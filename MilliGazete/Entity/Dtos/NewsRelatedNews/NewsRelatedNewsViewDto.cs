using System;

namespace Entity.Dtos
{
    public class NewsRelatedNewsViewDto
    {
        public int RelatedNewsId { get; set; }
        public virtual RelatedNewsViewDto RelatedNews { get; set; }
    }
}
