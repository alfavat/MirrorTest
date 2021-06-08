using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class MenuMapper : Profile
    {
        public MenuMapper()
        {
            CreateMap<MenuAddDto, Menu>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Active = true; })
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<MenuUpdateDto, Menu>()
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<Menu, MenuViewDto>().ForMember(f => f.ParentMenuTitle,
                u => u.MapFrom(g => g.ParentMenu == null ? "" : g.ParentMenu.Title))
                .ForMember(f => f.ChildrenMenuList, g => g.MapFrom(t => t.InverseParentMenu));

        }
    }
}
