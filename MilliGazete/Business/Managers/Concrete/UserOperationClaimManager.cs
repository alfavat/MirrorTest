using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimAssistantService _userOperationClaimAssistantService;

        public UserOperationClaimManager(IUserOperationClaimAssistantService userOperationClaimAssistantService)
        {
            _userOperationClaimAssistantService = userOperationClaimAssistantService;
        }


        [SecuredOperation("UserOperationClaimGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<UserOperationClaimViewDto>>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaimViewDto>>(await _userOperationClaimAssistantService.GetByUserId(userId));
        }

        [SecuredOperation("UserOperationClaimUpdate")]
        [CacheRemoveAspect("IUserOperationClaimService.Get")]
        [PerformanceAspect()]
        [ValidationAspect(typeof(UserOperationClaimUpdateValidator))]
        public async Task<IResult> Update(UserOperationClaimUpdateDto dto)
        {
            await _userOperationClaimAssistantService.Update(dto);
            return new SuccessResult(Messages.Updated);
        }
    }
}
