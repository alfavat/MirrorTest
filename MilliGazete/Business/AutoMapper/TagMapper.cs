using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<TagAddDto, Tag>()
                  .BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Active = true; })
                  .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));
            CreateMap<TagUpdateDto, Tag>()
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));
            CreateMap<Tag, TagDto>().ReverseMap();
        }
    }
}
