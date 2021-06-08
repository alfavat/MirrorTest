using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICategoryAssistantService
    {
        Task<Category> GetById(int categoryId);
        Task<Category> GetByUrl(string url);
        Task Update(Category category);
        Task Delete(Category category);
        Task<List<CategoryDto>> GetList();
        Task Add(Category category);
        List<CategoryDto> GetListByPaging(CategoryPagingDto pagingDto, out int total);
        Task<List<CategoryDto>> GetActiveList();
        Task<CategoryDto> GetViewById(int categoryId);
    }
}
