using Entity.Abstract;
using System;

namespace Entity.Dtos
{
    public class UserViewDto :IDto
    { 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Active { get; set; }
        public bool IsEmployee { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string FirstClaim { get; set; }
    }
}
