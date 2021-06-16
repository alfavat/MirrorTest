using Entity.Models;
using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class UserQuestionAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string IpAddress { get; set; }
        public TimeSpan CraetedAt { get; set; }

        public virtual QuestionAnswer Answer { get; set; }
        public virtual Question Question { get; set; }
    }
}
