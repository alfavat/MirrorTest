using DataAccess.Base;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsPositionDal : IEntityRepository<NewsPosition>
    {
        Task UpdateNewsPositions(List<NewsPositionUpdateDto> newsPositions);
        Task IncreaseNewsPositionOrdersByEntityId(int newsPositionEntityId);
        Task ReOrderNewsPositionOrdersByNewsId(int newsId);
        Task MoveSixteenthNewsToMainPageNewsPosition();
    }
}
