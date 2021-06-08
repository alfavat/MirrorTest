using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class UserCategoryRelationMapper : Profile
    {
        public UserCategoryRelationMapper()
        {
            CreateMap<UserCategoryRelation, UserCategoryRelationViewDto>()
                .ForMember(f=>f.Category , opt => opt.MapFrom(s => s.Category));
            CreateMap<UserCategoryRelationUpdateDto, UserCategoryRelation>().ReverseMap();
        }
    }
}
