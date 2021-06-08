namespace Entity.Dtos
{
    public class UserOperationClaimViewDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OperationClaimId { get; set; }

        public string OperationClaimName { get; set; }
        public string UserName { get; set; }
    }
}
