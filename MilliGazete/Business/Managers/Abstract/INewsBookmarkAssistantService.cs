using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsBookmarkAssistantService
    {
        Task<NewsBookmark> GetById(int newsId, int requestUserId);
        Task<List<NewsBookmarkDto>> GetList(int requestUserId);
        Task Add(NewsBookmark newsBookmark);
        Task<NewsBookmarkDto> GetByNewsUrl(string url, int requestUserId);
        Task<NewsBookmarkDto> GetByNewsId(int newsId, int requestUserId);
        Task DeleteByNewsId(int newsId, int requestUserId);
        Task<bool> HasUserBookmarkedNews(int newsId, int requestUserId);
    }
}
