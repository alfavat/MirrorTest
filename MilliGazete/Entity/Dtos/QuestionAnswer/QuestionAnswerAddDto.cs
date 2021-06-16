using Entity.Models;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class QuestionAnswerAddDto
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
