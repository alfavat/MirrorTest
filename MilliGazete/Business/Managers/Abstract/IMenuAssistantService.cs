using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IMenuAssistantService
    {
        Task<Menu> GetById(int id);
        Task Update(Menu menu);
        Task Delete(Menu menu);
        Task<List<MenuViewDto>> GetList();
        Task Add(Menu Menu);
        Task<List<MenuViewDto>> GetActiveList();
    }
}
