using Entity.Models;
using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<QuestionAnswerDto> QuestionAnswers { get; set; }
    }
}
