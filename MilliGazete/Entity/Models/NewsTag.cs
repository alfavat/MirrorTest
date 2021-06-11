using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class NewsTag
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int TagId { get; set; }

        public virtual News News { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
