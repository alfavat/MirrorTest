using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IQuestionAssistantService
    {
        Task<Question> GetById(int questionId);
        Task Update(Question question);
        Task Delete(Question question);
        Task<List<QuestionDto>> GetList();
        Task<Question> Add(Question question);
        Task<QuestionDto> GetViewById(int questionId);
    }
}
