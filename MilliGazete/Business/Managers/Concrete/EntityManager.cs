using Business.BusinessAspects.Autofac;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class EntityManager : IEntityService
    {
        private readonly IEntityAssistantService _entityAssistantService;
        public EntityManager(IEntityAssistantService EntityAssistant)
        {
            _entityAssistantService = EntityAssistant;
        }
        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<EntityDto>>> GetCounterEntities()
        {
            return new SuccessDataResult<List<EntityDto>>(await _entityAssistantService.GetByGroupId(EntityGroupType.CounterEntities));
        }
        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<EntityDto>>> GetNewsAgencyEntities()
        {
            return new SuccessDataResult<List<EntityDto>>(await _entityAssistantService.GetByGroupId(EntityGroupType.NewsAgencyEntities));
        }
        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<EntityDto>>> GetNewsFileTypeEntities()
        {
            return new SuccessDataResult<List<EntityDto>>(await _entityAssistantService.GetByGroupId(EntityGroupType.NewsFileTypeEntities));
        }
        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<EntityDto>>> GetNewsTypeEntities()
        {
            return new SuccessDataResult<List<EntityDto>>(await _entityAssistantService.GetByGroupId(EntityGroupType.NewsTypeEntities));
        }
        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<EntityDto>>> GetPositionEntities()
        {
            return new SuccessDataResult<List<EntityDto>>(await _entityAssistantService.GetByGroupId(EntityGroupType.PositionEntities));
        }
        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<EntityDto>>> GetPropertyEntities()
        {
            return new SuccessDataResult<List<EntityDto>>(await _entityAssistantService.GetByGroupId(EntityGroupType.PropertyEntities));
        }
    }
}
