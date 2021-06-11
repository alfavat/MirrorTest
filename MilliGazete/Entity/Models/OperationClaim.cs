﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class OperationClaim
    {
        public OperationClaim()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public int Id { get; set; }
        public string ClaimName { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
