using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Newspaper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ThumbnailFileId { get; set; }
        public int? MainImageFileId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }

        public virtual File MainImageFile { get; set; }
        public virtual File ThumbnailFile { get; set; }
    }
}
