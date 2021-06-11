using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class UserPasswordRequest
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual User User { get; set; }
    }
}
