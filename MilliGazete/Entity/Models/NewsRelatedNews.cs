using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class NewsRelatedNews
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int RelatedNewsId { get; set; }

        public virtual News News { get; set; }
        public virtual News RelatedNews { get; set; }
    }
}
