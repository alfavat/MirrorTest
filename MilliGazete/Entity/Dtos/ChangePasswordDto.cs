using Entity.Abstract;

namespace Entity.Dtos
{
    public class ChangePasswordDto : IDto
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
