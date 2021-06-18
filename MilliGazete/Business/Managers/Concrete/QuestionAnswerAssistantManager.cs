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
    public class QuestionAnswerAssistantManager : IQuestionAnswerAssistantService
    {
        private readonly IQuestionAnswerDal _questionAnswerDal;
        private readonly IMapper _mapper;
        public QuestionAnswerAssistantManager(IQuestionAnswerDal QuestionAnswerDal, IMapper mapper)
        {
            _questionAnswerDal = QuestionAnswerDal;
            _mapper = mapper;
        }

        public async Task<QuestionAnswer> GetById(int questionAnswerId)
        {
            return await _questionAnswerDal.Get(p => p.Id == questionAnswerId && !p.Deleted);
        }

        public async Task<QuestionAnswerDto> GetViewById(int questionAnswerId)
        {
            var data = _questionAnswerDal.GetList(p => p.Id == questionAnswerId && !p.Deleted);
            return await _mapper.ProjectTo<QuestionAnswerDto>(data).FirstOrDefaultAsync();
        }

        public async Task Update(QuestionAnswer questionAnswer)
        {
            await _questionAnswerDal.Update(questionAnswer);
        }

        public async Task Delete(QuestionAnswer questionAnswer)
        {
            questionAnswer.Deleted = true;
            await _questionAnswerDal.Update(questionAnswer);
        }

        public async Task Add(QuestionAnswer questionAnswer)
        {
            await _questionAnswerDal.Add(questionAnswer);
        }

        public async Task<List<QuestionAnswerDto>> GetList()
        {
            var list = _questionAnswerDal.GetList(p=> !p.Deleted);
            return await _mapper.ProjectTo<QuestionAnswerDto>(list).ToListAsync();
        }

        public async Task<List<QuestionAnswerDto>> GetListByQuestionId(int questionId)
        {
            var list = _questionAnswerDal.GetList(prop=>prop.QuestionId == questionId && !prop.Deleted);
            return await _mapper.ProjectTo<QuestionAnswerDto>(list).ToListAsync();
        }
    }
}
