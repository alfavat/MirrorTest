using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class NewsBookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NewsId { get; set; }

        public virtual News News { get; set; }
        public virtual User User { get; set; }
    }
}
