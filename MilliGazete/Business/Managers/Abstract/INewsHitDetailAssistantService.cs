using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsHitDetailAssistantService
    {
        Task<List<NewsHitDetailDto>> GetList();
        Task Add(NewsHitDetail data);
        Task<List<NewsHitDetailDto>> GetLastNewHitDetails(int minutes);
        Task<List<NewsHitDetailDto>> GetListByNewsId(int newsId);
    }
}
