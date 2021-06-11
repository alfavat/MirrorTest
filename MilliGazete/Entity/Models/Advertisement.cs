using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Advertisement
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string GoogleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
