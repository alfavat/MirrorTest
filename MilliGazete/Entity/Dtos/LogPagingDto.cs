using System;

namespace Entity.Dtos
{
    public class LogPagingDto : PagingDto
    {
        public DateTime? FromCreateDate { get; set; }
        public DateTime? ToCreateDate { get; set; }
        public int? UserId { get; set; }
        public string AuditType { get; set; }
    }
}
