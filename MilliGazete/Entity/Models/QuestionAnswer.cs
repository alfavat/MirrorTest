using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class QuestionAnswer
    {
        public QuestionAnswer()
        {
            UserQuestionAnswers = new HashSet<UserQuestionAnswer>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public bool Deleted { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<UserQuestionAnswer> UserQuestionAnswers { get; set; }
    }
}
