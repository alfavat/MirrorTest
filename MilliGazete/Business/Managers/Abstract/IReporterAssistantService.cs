using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IReporterAssistantService
    {
        Task<Reporter> GetById(int reporterId);
        Task Update(Reporter reporter);
        Task Delete(Reporter reporter);
        Task<List<ReporterDto>> GetList();
        Task<Reporter> Add(Reporter reporter);
        Task<ReporterDto> GetViewById(int reporterId);
    }
}
