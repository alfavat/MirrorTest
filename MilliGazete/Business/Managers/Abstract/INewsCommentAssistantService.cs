using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsCommentAssistantService
    {
        Task<NewsComment> GetById(int newsCommentId);
        Task Update(NewsComment newsComment);
        Task Delete(NewsComment newsComment);
        Task<List<NewsCommentDto>> GetList();
        Task Add(NewsComment newsComment);
        List<NewsCommentDto> GetListByPaging(NewsCommentPagingDto pagingDto, out int total);
        List<UserNewsCommentDto> GetByNewsId(int newsId, int limit, int page,out int total);
    }
}
