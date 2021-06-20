using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsFileAssistantService
    {
        Task<NewsFile> GetById(int newsFileId);
        List<NewsFileDto> GetListByPaging(NewsFilePagingDto pagingDto, out int total);
        Task<NewsFileDto> GetViewById(int newsFileId);
    }
}
