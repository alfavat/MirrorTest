using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsPositionAssistantService
    {
        Task UpdateNewsPositionOrders(List<NewsPositionUpdateDto> newsPositions);
        Task<List<NewsPositionDto>> GetOrdersByNewsPositionEntityId(int newsPositionEntityId, int limit);
        Task IncreaseNewsPositionOrdersByEntityId(int newsPositionEntityId);
        Task ReOrderNewsPositionOrdersByNewsId(int id);
        Task MoveSixteenthNewsToMainPageNewsPosition();
    }
}
