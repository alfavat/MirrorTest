using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Question
    {
        public Question()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
            UserQuestionAnswers = new HashSet<UserQuestionAnswer>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<UserQuestionAnswer> UserQuestionAnswers { get; set; }
    }
}
