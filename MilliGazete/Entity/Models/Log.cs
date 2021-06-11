using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Detail { get; set; }
        public string Audit { get; set; }
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
