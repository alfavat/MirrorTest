using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewspaperService
    {
        Task<IResult> AddArray(List<NewspaperAddDto> dto);
        Task<IDataResult<List<NewspaperDto>>> GetTodayList();
        Task<IDataResult<NewspaperDto>> GetByName(string name);
    }
}
