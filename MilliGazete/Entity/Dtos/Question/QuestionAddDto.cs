using Entity.Models;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class QuestionAddDto
    {
        public string QuestionText { get; set; }
        public bool Active { get; set; }
    }
}
