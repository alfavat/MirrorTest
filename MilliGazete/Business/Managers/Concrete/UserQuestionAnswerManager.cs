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
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserQuestionAnswerManager : IUserQuestionAnswerService
    {
        private readonly IUserQuestionAnswerAssistantService _userQuestionAnswerAssistantService;
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public UserQuestionAnswerManager(IUserQuestionAnswerAssistantService userQuestionAnswerAssistantService, 
            IQuestionAnswerService questionAnswerService,
            IHttpContextAccessor httpContext,
            IMapper mapper)
        {
            _userQuestionAnswerAssistantService = userQuestionAnswerAssistantService;
            _questionAnswerService = questionAnswerService;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<UserQuestionAnswerDto>>> GetList()
        {
            return new SuccessDataResult<List<UserQuestionAnswerDto>>(await _userQuestionAnswerAssistantService.GetList());
        }

        [SecuredOperation("UserQuestionAnswerGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<UserQuestionAnswerDto>> GetById(int id)
        {
            var data = await _userQuestionAnswerAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<UserQuestionAnswerDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<UserQuestionAnswerDto>(data);
        }

        [ValidationAspect(typeof(UserQuestionAnswerAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IUserQuestionAnswerService.Get")]
        public async Task<IResult> Add(UserQuestionAnswerAddDto dto)
        {
            var checkAnswerRecord = await _questionAnswerService.GetById(dto.AnswerId);
            if (!checkAnswerRecord.Success)
                return new ErrorResult(Messages.RecordNotFound);

            var data = _mapper.Map<UserQuestionAnswer>(dto);
            data.QuestionId = checkAnswerRecord.Data.QuestionId;
            data.IpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();

            await _userQuestionAnswerAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }
    }
}
