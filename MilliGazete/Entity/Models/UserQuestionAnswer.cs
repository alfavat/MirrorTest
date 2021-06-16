using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class UserQuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual QuestionAnswer Answer { get; set; }
        public virtual Question Question { get; set; }
    }
}
