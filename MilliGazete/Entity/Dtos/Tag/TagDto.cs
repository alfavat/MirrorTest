using System;

namespace Entity.Dtos
{
    public class TagDto
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
