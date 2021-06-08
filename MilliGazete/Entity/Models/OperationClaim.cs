using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class OperationClaim
    {
        public OperationClaim()
        {
            UserOperationClaim = new HashSet<UserOperationClaim>();
        }

        public int Id { get; set; }
        public string ClaimName { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaim { get; set; }
    }
}
