using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface INewspaperRepository : IUIBaseRepository
    {
        Task<IDataResult<List<NewspaperDto>>> GetList();
        Task<IDataResult<NewspaperDto>> GetMilliGazeteNewspaper();
    }
}
