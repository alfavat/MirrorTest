using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IQuestionAnswerService
    {
        Task<IDataResult<QuestionAnswerDto>> GetById(int questionAnswerId);
        Task<IResult> Update(QuestionAnswerUpdateDto questionAnswerUpdateDto);
        Task<IResult> Add(QuestionAnswerAddDto questionAnswerAddDto);
        Task<IResult> Delete(int questionAnswerId);
        Task<IDataResult<List<QuestionAnswerDto>>> GetList();
        Task<IDataResult<List<QuestionAnswerDto>>> GetListByQuestionId(int questionId);
    }
}
