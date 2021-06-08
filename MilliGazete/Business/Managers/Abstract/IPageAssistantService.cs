using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IPageAssistantService
    {
        Task<Page> GetById(int pageId);
        Task<PageDto> GetByUrl(string url);
        Task Update(Page page);
        Task Delete(Page page);
        Task<List<PageDto>> GetList();
        Task Add(Page page);
        List<PageDto> GetListByPaging(PagePagingDto pagingDto, out int total);
        Task<PageDto> GetViewById(int id);
    }
}
