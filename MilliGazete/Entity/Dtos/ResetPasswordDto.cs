using Entity.Abstract;

namespace Entity.Dtos
{
    public class ResetPasswordDto : IDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
