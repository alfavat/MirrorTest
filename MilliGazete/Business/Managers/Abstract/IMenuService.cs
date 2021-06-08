using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IMenuService
    {
        Task<IDataResult<MenuViewDto>> GetById(int id);
        Task<IResult> Update(MenuUpdateDto menuUpdateDto);
        Task<IResult> Add(MenuAddDto menuAddDto);
        Task<IResult> Delete(int id);
        Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto);
        Task<IDataResult<List<MenuViewDto>>> GetList();
        Task<IDataResult<List<MenuViewDto>>> GetActiveList();
    }
}
