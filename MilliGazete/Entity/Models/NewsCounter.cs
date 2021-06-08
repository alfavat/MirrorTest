using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class NewsCounter
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int CounterEntityId { get; set; }
        public int? Value { get; set; }

        public virtual Entity CounterEntity { get; set; }
        public virtual News News { get; set; }
    }
}
