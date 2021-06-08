using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetById(int categoryId);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto);
        Task<IResult> Add(CategoryAddDto categoryAddDto);
        Task<IResult> Delete(int categoryId);
        Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto);
        Task<IDataResult<List<CategoryDto>>> GetList();
        IDataResult<List<CategoryDto>> GetListByPaging(CategoryPagingDto pagingDto, out int total);
        Task<IDataResult<List<CategoryDto>>> GetActiveList();
    }
}
