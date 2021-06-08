using DataAccess.Base;
using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsPositionDal : IEntityRepository<NewsPosition>
    {
        Task UpdateNewsPositions(List<NewsPositionUpdateDto> newsPositions);
        IQueryable<NewsPosition> GetListWithDetails(Expression<Func<NewsPosition, bool>> filter = null);
        Task IncreaseNewsPositionOrdersByEntityId(int newsPositionEntityId);
        Task ReOrderNewsPositionOrdersByNewsId(int newsId);
        Task MoveSixteenthNewsToMainPageNewsPosition();
    }
}
