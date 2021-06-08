using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class NewsPosition
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int PositionEntityId { get; set; }
        public bool? Value { get; set; }
        public int? Order { get; set; }

        public virtual News News { get; set; }
        public virtual Entity PositionEntity { get; set; }
    }
}
