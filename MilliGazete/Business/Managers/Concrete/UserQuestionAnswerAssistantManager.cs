using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserQuestionAnswerAssistantManager : IUserQuestionAnswerAssistantService
    {
        private readonly IUserQuestionAnswerDal _userQuestionAnswerDal;
        private readonly IMapper _mapper;
        public UserQuestionAnswerAssistantManager(IUserQuestionAnswerDal UserQuestionAnswerDal, IMapper mapper)
        {
            _userQuestionAnswerDal = UserQuestionAnswerDal;
            _mapper = mapper;
        }

        public async Task<UserQuestionAnswer> GetById(int userQuestionAnswerId)
        {
            return await _userQuestionAnswerDal.Get(p => p.Id == userQuestionAnswerId);
        }

        public async Task<UserQuestionAnswerDto> GetViewById(int userQuestionAnswerId)
        {
            var data = _userQuestionAnswerDal.GetList(p => p.Id == userQuestionAnswerId).Include(f => f.Question).Include(f=>f.Answer);
            return await _mapper.ProjectTo<UserQuestionAnswerDto>(data).FirstOrDefaultAsync();
        }

        public async Task Add(UserQuestionAnswer userQuestionAnswer)
        {
            await _userQuestionAnswerDal.Add(userQuestionAnswer);
        }

        public async Task<List<UserQuestionAnswerDto>> GetList()
        {
            var list = _userQuestionAnswerDal.GetList().Include(f => f.Question).Include(f => f.Answer);
            return await _mapper.ProjectTo<UserQuestionAnswerDto>(list).ToListAsync();
        }

        public async Task<UserQuestionAnswer> GetIsAnswered(int questionId, string ipAddress)
        {
            return await _userQuestionAnswerDal.Get(prop => prop.IpAddress == ipAddress && prop.QuestionId == questionId);
        }
    }
}
