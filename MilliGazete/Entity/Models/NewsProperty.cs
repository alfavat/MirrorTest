using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class NewsProperty
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int PropertyEntityId { get; set; }
        public bool? Value { get; set; }

        public virtual News News { get; set; }
        public virtual Entity PropertyEntity { get; set; }
    }
}
