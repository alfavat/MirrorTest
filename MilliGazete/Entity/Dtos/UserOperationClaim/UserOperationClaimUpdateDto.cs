using System.Collections.Generic;

namespace Entity.Dtos
{
    public class UserOperationClaimUpdateDto
    {
        public int? UserId { get; set; }
        public List<int> OperationClaimIds { get; set; }
    }
}
