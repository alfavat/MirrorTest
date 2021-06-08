using System;

namespace Entity.Dtos
{
    public class LogDto
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Audit { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }
}
