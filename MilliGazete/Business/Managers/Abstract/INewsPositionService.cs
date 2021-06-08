using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsPositionService
    {
        Task<IResult> UpdateNewsPositionOrders(List<NewsPositionUpdateDto> newsPositions);
        Task<IDataResult<List<NewsPositionDto>>> GetOrdersByNewsPositionEntityId(int newsPositionEntityId, int limit);
    }
}
