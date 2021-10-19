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
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorAssistantService _authorAssistantService;
        private readonly IMapper _mapper;

        public AuthorManager(IAuthorAssistantService AuthorAssistantService, IMapper mapper)
        {
            _authorAssistantService = AuthorAssistantService;
            _mapper = mapper;
        }

        [PerformanceAspect()]
        public async Task<IDataResult<List<AuthorDto>>> GetList()
        {
            return new SuccessDataResult<List<AuthorDto>>(await _authorAssistantService.GetList());
        }

        [SecuredOperation("AuthorGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<AuthorDto>> GetById(int id)
        {
            var data = await _authorAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<AuthorDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<AuthorDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<AuthorWithDetailsDto>> GetByName(string nameSurename)
        {
            var data = await _authorAssistantService.GetViewByName(nameSurename);
            if (data == null)
            {
                return new ErrorDataResult<AuthorWithDetailsDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<AuthorWithDetailsDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<AuthorWithDetailsDto>> GetByUrl(string url)
        {
            var data = await _authorAssistantService.GetViewByUrl(url);
            if (data == null)
            {
                return new ErrorDataResult<AuthorWithDetailsDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<AuthorWithDetailsDto>(data);
        }

        [SecuredOperation("AuthorUpdate")]
        [ValidationAspect(typeof(AuthorUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IAuthorService.Get")]
        public async Task<IResult> Update(AuthorUpdateDto authorUpdateDto)
        {
            var data = await _authorAssistantService.GetById(authorUpdateDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(authorUpdateDto, data);
            await _authorAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("AuthorAdd")]
        [ValidationAspect(typeof(AuthorAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IAuthorService.Get")]
        public async Task<IResult> Add(AuthorAddDto authorAddDto)
        {
            var exists = await _authorAssistantService.CheckAuthor(authorAddDto);
            if (exists != null)
            {
                var msg = exists.NameSurename.ToLower() == authorAddDto.NameSurename.ToLower() ? Messages.NameSurenameAlreadyExists :
                    exists.Email.ToLower() == authorAddDto.Email.ToLower() ? Messages.EmailAlreadyExists :
                    Messages.UrlAlreadyExists;
                return new ErrorResult(msg);
            }
            var data = _mapper.Map<Author>(authorAddDto);
            await _authorAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("AuthorDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IAuthorService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _authorAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Deleted = true;
            await _authorAssistantService.Update(data);
            return new SuccessResult(Messages.Deleted);
        }
    }
}
