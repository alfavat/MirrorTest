using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class NewsHit
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int NewsHitComeFromEntityId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual News News { get; set; }
    }
}
