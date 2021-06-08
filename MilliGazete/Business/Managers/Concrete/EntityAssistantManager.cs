using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class EntityAssistantManager : IEntityAssistantService
    {
        private readonly IMapper _mapper;
        private readonly IEntityDal _entityDal;
        public EntityAssistantManager(IEntityDal EntityDal, IMapper mapper)
        {
            _entityDal = EntityDal;
            _mapper = mapper;
        }

        public async Task<List<EntityDto>> GetByGroupId(EntityGroupType entityGroupType)
        {
            var groupId = (int)entityGroupType;
            var data = _entityDal.GetList(f => f.EntityGroupId == groupId);
            var list = await _mapper.ProjectTo<EntityDto>(data).ToListAsync();
            return list.OrderBy(f => f.EntityName).ToList();
        }
    }
}
