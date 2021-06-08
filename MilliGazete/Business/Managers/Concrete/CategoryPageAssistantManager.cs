using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class CategoryPageAssistantManager : ICategoryPageAssistantService
    {
        private readonly IMapper _mapper;
        private readonly INewsDal _newsDal;

        public CategoryPageAssistantManager(INewsDal NewsDal, IMapper mapper)
        {
            _newsDal = NewsDal;
            _mapper = mapper;
        }

        public async Task<List<MainPageCategoryNewsDto>> GetLastNewsByCategoryUrl(string url, int limit)
        {
            var query = _newsDal.GetNewsListByCategoryUrl(url, out int headingPositionEntityId);
            query = query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            query = query
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.Author).AsQueryable();

            return await _mapper.ProjectTo<MainPageCategoryNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainHeadingDto>> GetTopMainHeadingNewsByCategoryUrl(string url, int limit)
        {
            var query = _newsDal.GetNewsListByCategoryUrl(url, out int headingPositionEntityId);

            query = query.Where(u => u.NewsPosition.Any(u => u.PositionEntityId == headingPositionEntityId && u.Order > 0));
            query = query.OrderBy(u => u.NewsPosition.First(u => u.PositionEntityId == headingPositionEntityId).Order).Take(limit.CheckLimit())
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.NewsPosition)
                .Include(f => f.Author).AsQueryable();
            return await _mapper.ProjectTo<MainHeadingDto>(query).ToListAsync();
        }

        public async Task<List<MainPageCategoryNewsDto>> GetLastNewsByCategoryId(int id, int limit)
        {
            var query = _newsDal.GetNewsListByCategoryId(id, out int headingPositionEntityId);
            query = query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            query = query
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.Author).AsQueryable();

            return await _mapper.ProjectTo<MainPageCategoryNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainHeadingDto>> GetTopMainHeadingNewsByCategoryId(int id, int limit)
        {
            var query = _newsDal.GetNewsListByCategoryId(id, out int headingPositionEntityId);

            query = query.Where(u => u.NewsPosition.Any(u => u.PositionEntityId == headingPositionEntityId && u.Order > 0));
            query = query.OrderBy(u => u.NewsPosition.First(u => u.PositionEntityId == headingPositionEntityId).Order).Take(limit.CheckLimit())
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.Author).AsQueryable();
            return await _mapper.ProjectTo<MainHeadingDto>(query).ToListAsync();
        }

    }
}
