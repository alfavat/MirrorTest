using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IPageService
    {
        Task<IDataResult<PageDto>> GetById(int pageId);
        Task<IResult> Update(PageUpdateDto pageUpdateDto);
        Task<IResult> Add(PageAddDto pageAddDto);
        Task<IResult> Delete(int pageId);
        IDataResult<List<PageDto>> GetListByPaging(PagePagingDto pagingDto, out int total);
        Task<IDataResult<PageDto>> GetByUrl(string url);
        Task<IDataResult<List<PageDto>>> GetList();
    }
}
