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
        Task ReOrderNewsPositionOrdersByNewsId(int newsId);
    }
}
