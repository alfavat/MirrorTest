using DataAccess.Base;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAgencyNewsDal : IEntityRepository<AgencyNews>
    {
        Task DeleteAllByAgencyId(int newsAgencyEntityId);
        Task AddWithFiles(AgencyNews agencyNews, List<AgencyNewsFile> files);
        Task<AgencyNewsFile> GetAgencyNewsFileById(int id);
    }
}
