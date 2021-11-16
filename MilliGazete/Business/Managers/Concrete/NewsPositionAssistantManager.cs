using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsPositionAssistantManager : INewsPositionAssistantService
    {
        private readonly INewsPositionDal _newsPositionDal;
        private readonly IMapper _mapper;
        public NewsPositionAssistantManager(INewsPositionDal newsPositionDal, IMapper mapper)
        {
            _newsPositionDal = newsPositionDal;
            _mapper = mapper;
        }

        public async Task UpdateNewsPositionOrders(List<NewsPositionUpdateDto> newsPositions)
        {
            await _newsPositionDal.UpdateNewsPositions(newsPositions);
        }

        public async Task<List<NewsPositionDto>> GetOrdersByNewsPositionEntityId(int newsPositionEntityId, int limit)
        {
            var list = _newsPositionDal.GetList(f =>
            f.PositionEntityId == newsPositionEntityId && !f.News.Deleted && f.News.Active &&
            f.News.Approved.Value && !f.News.IsDraft && f.News.IsLastNews && f.Order > 0)
                .OrderBy(f => f.Order)
                .Take(limit.CheckLimit())
                .Include(f => f.News).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .AsQueryable();
            return await _mapper.ProjectTo<NewsPositionDto>(list).ToListAsync();
        }

        public async Task IncreaseNewsPositionOrdersByEntityId(int newsPositionEntityId)
        {
            await _newsPositionDal.IncreaseNewsPositionOrdersByEntityId(newsPositionEntityId);
        }

        public async Task ReOrderNewsPositionOrdersByNewsId(int id)
        {
            await _newsPositionDal.ReOrderNewsPositionOrdersByNewsId(id);
        }

        public async Task MoveSixteenthNewsToMainPageNewsPosition()
        {
            await _newsPositionDal.MoveSixteenthNewsToMainPageNewsPosition();
        }
    }
}
