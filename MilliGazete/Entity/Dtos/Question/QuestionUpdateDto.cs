using System.Collections.Generic;

namespace Entity.Dtos
{
    public class QuestionUpdateDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Active { get; set; }
    }
}
