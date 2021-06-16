using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserQuestionAnswerAssistantService
    {
        Task<UserQuestionAnswer> GetById(int userQuestionAnswerId);
        Task<List<UserQuestionAnswerDto>> GetList();
        Task Add(UserQuestionAnswer userQuestionAnswer);
        Task<UserQuestionAnswerDto> GetViewById(int userQuestionAnswerId);
    }
}
