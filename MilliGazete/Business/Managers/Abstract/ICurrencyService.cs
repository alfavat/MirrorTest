using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICurrencyService
    {
        Task<IDataResult<List<CurrencyDto>>> GetList();
        Task<IResult> AddArray(List<CurrencyAddDto> list);
    }
}
