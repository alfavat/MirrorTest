using System;

namespace Entity.Dtos
{
    public class NewsHitDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int NewsHitComeFromEntityId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
