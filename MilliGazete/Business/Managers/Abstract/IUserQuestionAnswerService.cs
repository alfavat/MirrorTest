using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserQuestionAnswerService
    {
        Task<IDataResult<UserQuestionAnswerDto>> GetById(int userQuestionAnswerId);
        Task<IResult> Add(UserQuestionAnswerAddDto userQuestionAnswerAddDto);
        Task<IDataResult<List<UserQuestionAnswerDto>>> GetList();
    }
}
