using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IQuestionAnswerAssistantService
    {
        Task<QuestionAnswer> GetById(int questionAnswerId);
        Task Update(QuestionAnswer questionAnswer);
        Task Delete(QuestionAnswer questionAnswer);
        Task<List<QuestionAnswerDto>> GetList();
        Task Add(QuestionAnswer questionAnswer);
        Task<QuestionAnswerDto> GetViewById(int questionAnswerId);
    }
}
