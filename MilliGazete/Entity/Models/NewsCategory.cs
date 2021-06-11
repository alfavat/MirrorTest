using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class NewsCategory
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual News News { get; set; }
    }
}
