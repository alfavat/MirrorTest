using System.Collections.Generic;

namespace Entity.Dtos
{
    public class UserCategoryRelationUpdateDto
    {
        public int UserId { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
