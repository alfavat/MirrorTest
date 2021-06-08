using Entity.Abstract;
using System;

namespace Entity.Dtos
{
    public class UserForLoginDto : IDto
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
}
