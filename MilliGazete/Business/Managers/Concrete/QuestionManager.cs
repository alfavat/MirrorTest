using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionAssistantService _questionAssistantService;
        private readonly IMapper _mapper;

        public QuestionManager(IQuestionAssistantService questionAssistantService, IMapper mapper)
        {
            _questionAssistantService = questionAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<QuestionDto>>> GetList()
        {
            return new SuccessDataResult<List<QuestionDto>>(await _questionAssistantService.GetList());
        }

        [SecuredOperation("QuestionGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<QuestionDto>> GetById(int id)
        {
            var data = await _questionAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<QuestionDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<QuestionDto>(data);
        }

        [SecuredOperation("QuestionUpdate")]
        [ValidationAspect(typeof(QuestionUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IQuestionService.Get")]
        public async Task<IResult> Update(QuestionUpdateDto dto)
        {
            var data = await _questionAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, data);
            await _questionAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("QuestionAdd")]
        [ValidationAspect(typeof(QuestionAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IQuestionService.Get")]
        public async Task<IDataResult<int>> Add(QuestionAddDto dto)
        {
            var data = _mapper.Map<Question>(dto);
            var result = await _questionAssistantService.Add(data);
            return new SuccessDataResult<int>(result.Id,Messages.Added);
        }

        [SecuredOperation("QuestionDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IQuestionService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _questionAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            await _questionAssistantService.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
