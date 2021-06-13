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
    public class ContactManager : IContactService
    {
        private readonly IContactAssistantService _contactAssistantService;
        private readonly IMapper _mapper;

        public ContactManager(IContactAssistantService contactAssistantService, IMapper mapper)
        {
            _contactAssistantService = contactAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("ContactGet")]
        [PerformanceAspect()]
        public IDataResult<List<ContactDto>> GetListByPaging(ContactPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<ContactDto>>(_contactAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("ContactGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<ContactDto>>> GetList()
        {
            return new SuccessDataResult<List<ContactDto>>(await _contactAssistantService.GetList());
        }

        [SecuredOperation("ContactGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<ContactDto>> GetById(int contactId)
        {
            var data = await _contactAssistantService.GetViewById(contactId);
            if (data == null)
            {
                return new ErrorDataResult<ContactDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<ContactDto>(data);
        }

        [ValidationAspect(typeof(ContactAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IContactService.Get")]
        public async Task<IResult> Add(ContactAddDto contactAddDto)
        {
            var contact = _mapper.Map<Contact>(contactAddDto);
            await _contactAssistantService.Add(contact);
            return new SuccessResult(Messages.Added);
        }
    }
}
