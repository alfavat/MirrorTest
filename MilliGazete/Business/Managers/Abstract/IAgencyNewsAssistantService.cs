using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAgencyNewsAssistantService
    {
        Task<AgencyNews> GetById(int agencyNewsId);
        Task<AgencyNews> GetByCode(string code);
        Task Delete(AgencyNews agencyNews);
        Task<List<AgencyNewsViewDto>> GetList();
        List<AgencyNewsViewDto> GetListByPaging(NewsAgencyPagingDto pagingDto, out int total);
        Task DeleteAllByAgencyId(int agencyId);
        Task AddArray(List<NewsAgencyAddDto> data);
        Task<AgencyNewsFile> GetAgencyNewsFileById(int agencyNewsFileId);
        Task AddFile(File file);
        Task<List<NewsPropertyAddDto>> GetNewsPropertyEntities();
    }
}
