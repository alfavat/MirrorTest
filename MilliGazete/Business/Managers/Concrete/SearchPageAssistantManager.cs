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
    public class SearchPageAssistantManager : ISearchPageAssistantService
    {
        private readonly IMapper _mapper;
        private readonly INewsDal _newsDal;

        public SearchPageAssistantManager(INewsDal NewsDal, IMapper mapper)
        {
            _newsDal = NewsDal;
            _mapper = mapper;
        }

        public async Task<List<MainPageTagNewsDto>> GetNewsByTagUrl(string url, int limit)
        {
            var query = _newsDal.GetNewsListByTagUrl(url);
            query = query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            query = query.Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.Author)
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsTag).ThenInclude(f => f.Tag);
            return await _mapper.ProjectTo<MainPageTagNewsDto>(query).ToListAsync();
        }

        public List<MainPageSearchNewsDto> GetNewsListByPaging(NewsPagingDto pagingDto, out int total)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.NewsTag).ThenInclude(f => f.Tag)
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f=>f.Author)
                .Include(f => f.NewsPosition)
                .AsQueryable();

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.HtmlContent.ToLower().Contains(pagingDto.Query.ToLower()) ||
                f.ShortDescription.ToLower().Contains(pagingDto.Query.ToLower()));
            total = query.Count();
            var list = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit()).ToList();

            return _mapper.Map<List<MainPageSearchNewsDto>>(list);
        }
    }
}
