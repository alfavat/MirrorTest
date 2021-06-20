using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsFileService
    {
        Task<IDataResult<NewsFileDto>> GetById(int newsFileId);
        IDataResult<List<NewsFileDto>> GetListByPaging(NewsFilePagingDto pagingDto, out int total);
    }
}
