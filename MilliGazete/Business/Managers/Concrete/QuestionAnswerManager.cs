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
    public class QuestionAnswerManager : IQuestionAnswerService
    {
        private readonly IQuestionAnswerAssistantService _questionAnswerAssistantService;
        private readonly IMapper _mapper;

        public QuestionAnswerManager(IQuestionAnswerAssistantService questionAnswerAssistantService, IMapper mapper)
        {
            _questionAnswerAssistantService = questionAnswerAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<QuestionAnswerDto>>> GetList()
        {
            return new SuccessDataResult<List<QuestionAnswerDto>>(await _questionAnswerAssistantService.GetList());
        }

        [SecuredOperation("QuestionAnswerGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<QuestionAnswerDto>> GetById(int id)
        {
            var data = await _questionAnswerAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<QuestionAnswerDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<QuestionAnswerDto>(data);
        }

        [SecuredOperation("QuestionAnswerUpdate")]
        [ValidationAspect(typeof(QuestionAnswerUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IQuestionAnswerService.Get")]
        public async Task<IResult> Update(QuestionAnswerUpdateDto dto)
        {
            var data = await _questionAnswerAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, data);
            await _questionAnswerAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("QuestionAnswerAdd")]
        [ValidationAspect(typeof(QuestionAnswerAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IQuestionAnswerService.Get")]
        public async Task<IResult> Add(QuestionAnswerAddDto dto)
        {
            var data = _mapper.Map<QuestionAnswer>(dto);
            await _questionAnswerAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("QuestionAnswerDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IQuestionAnswerService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _questionAnswerAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            await _questionAnswerAssistantService.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
