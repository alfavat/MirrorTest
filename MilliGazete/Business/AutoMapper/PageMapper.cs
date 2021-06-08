using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class PageMapper : Profile
    {
        public PageMapper()
        {
            CreateMap<PageAddDto, Page>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; })
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<PageUpdateDto, Page>()
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<Page, PageDto>()
                .ForMember(f => f.FeaturedImageFile, g => g.MapFrom(t => t.FeaturedImageFile == null ? null : t.FeaturedImageFile));

        }
    }
}
