namespace Entity.Dtos
{
    public class UserCategoryRelationViewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
