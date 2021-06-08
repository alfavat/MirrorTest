using AutoMapper;
using Entity.Dtos;

namespace Business.AutoMapper
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<Entity.Models.Entity, EntityDto>().ForMember(f => f.EntityName, g => g.MapFrom(t => Translator.GetByKey(t.EntityName)));
        }
    }
}
