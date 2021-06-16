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
    public class QuestionAssistantManager : IQuestionAssistantService
    {
        private readonly IQuestionDal _questionDal;
        private readonly IMapper _mapper;
        public QuestionAssistantManager(IQuestionDal QuestionDal, IMapper mapper)
        {
            _questionDal = QuestionDal;
            _mapper = mapper;
        }

        public async Task<Question> GetById(int questionId)
        {
            return await _questionDal.Get(p => p.Id == questionId);
        }

        public async Task<QuestionDto> GetViewById(int questionId)
        {
            var data = _questionDal.GetList(p => p.Id == questionId).Include(f => f.QuestionAnswers);
            return await _mapper.ProjectTo<QuestionDto>(data).FirstOrDefaultAsync();
        }

        public async Task Update(Question question)
        {
            await _questionDal.Update(question);
        }

        public async Task Delete(Question question)
        {
            await _questionDal.Delete(question);
        }

        public async Task Add(Question question)
        {
            await _questionDal.Add(question);
        }

        public async Task<List<QuestionDto>> GetList()
        {
            var list = _questionDal.GetList().Include(f => f.QuestionAnswers);
            return await _mapper.ProjectTo<QuestionDto>(list).ToListAsync();
        }
    }
}
