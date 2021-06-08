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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserCategoryRelationManager : IUserCategoryRelationService
    {
        private readonly IUserCategoryRelationAssistantService _userCategoryRelationAssistantService;
        private readonly IMapper _mapper;

        public UserCategoryRelationManager(IUserCategoryRelationAssistantService UserCategoryRelationAssistantService, IMapper mapper)
        {
            _userCategoryRelationAssistantService = UserCategoryRelationAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("UserCategoryRelationGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<UserCategoryRelationViewDto>>> GetListByUserId(int userId)
        {
            return new SuccessDataResult<List<UserCategoryRelationViewDto>>(await _userCategoryRelationAssistantService.GetByUserId(userId));
        }


        [SecuredOperation("UserCategoryRelationUpdate")]
        [ValidationAspect(typeof(UserCategoryRelationUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IUserCategoryRelationService.Get")]
        public async Task<IResult> Update(UserCategoryRelationUpdateDto userCategoryRelationUpdateDto)
        {
            await _userCategoryRelationAssistantService.Update(userCategoryRelationUpdateDto);
            return new SuccessResult(Messages.Updated);
        }
    }
}
