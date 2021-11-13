using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class MenuAssistantManager : IMenuAssistantService
    {
        private readonly IMenuDal _menuDal;
        private readonly IMapper _mapper;
        public MenuAssistantManager(IMenuDal menuDal, IMapper mapper)
        {
            _menuDal = menuDal;
            _mapper = mapper;
        }
        public async Task<List<MenuViewDto>> GetActiveList()
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var data = await _mapper.ProjectTo<MenuViewDto>(_menuDal.GetList(f => !f.Deleted && f.Active && (languageId == 0 || f.LanguageId == languageId))).ToListAsync();
            if (data.HasValue())
            {
                var mainMenus = data.Where(f => !f.ParentMenuId.HasValue).ToList();
                if (mainMenus.HasValue())
                {
                    foreach (var item in mainMenus)
                    {
                        item.ChildrenMenuList = GetChildren(item.Id, data);
                    }
                }
                return mainMenus;
            }
            return data;
        }

        private List<MenuViewDto> GetChildren(int parentId, List<MenuViewDto> data)
        {
            var children = data.Where(f => f.ParentMenuId == parentId).ToList();
            if (children.HasValue())
            {
                foreach (var item in children)
                {
                    item.ChildrenMenuList = GetChildren(item.Id, data);
                }
            }
            return children;
        }

        public async Task<Menu> GetById(int id)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            return await _menuDal.Get(p => p.Id == id && !p.Deleted && (languageId == 0 || p.LanguageId == languageId));
        }

        public async Task Update(Menu menu)
        {
            await _menuDal.Update(menu);
        }

        public async Task Delete(Menu menu)
        {
            await _menuDal.Delete(menu);
        }

        public async Task Add(Menu menu)
        {
            await _menuDal.Add(menu);
        }

        public async Task<List<MenuViewDto>> GetList()
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var list = _menuDal.GetList(p => !p.Deleted && (languageId == 0 || p.LanguageId == languageId));
            return await _mapper.ProjectTo<MenuViewDto>(list).ToListAsync();
        }
    }
}
