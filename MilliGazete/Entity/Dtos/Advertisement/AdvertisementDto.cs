using System;

namespace Entity.Dtos
{
    public class AdvertisementDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string GoogleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
