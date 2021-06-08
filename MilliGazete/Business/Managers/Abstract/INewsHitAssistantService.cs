using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsHitAssistantService
    {
        Task<List<NewsHitDto>> GetList();
        Task<List<NewsHitDto>> GetListByNewsId(int newsId);
        Task AddWithDetail(NewsHitDetailAddDto dto);
    }
}
