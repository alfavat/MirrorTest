using Entity.Models;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class QuestionAnswerUpdateDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
