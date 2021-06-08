using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class PageAssistantManager : IPageAssistantService
    {
        private readonly IPageDal _pageDal;
        private readonly IMapper _mapper;
        public PageAssistantManager(IPageDal PageDal, IMapper mapper)
        {
            _pageDal = PageDal;
            _mapper = mapper;
        }
        public List<PageDto> GetListByPaging(PagePagingDto pagingDto, out int total)
        {
            var list = _pageDal.GetList(f => !f.Deleted).Include(f => f.FeaturedImageFile);
            var query = _mapper.ProjectTo<PageDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.SeoTitle.Contains(pagingDto.Query) || f.Url.Contains(pagingDto.Query) ||
                f.SeoDescription.Contains(pagingDto.Query) || f.SeoKeywords.Contains(pagingDto.Query));

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<Page> GetById(int pageId)
        {
            return await _pageDal.Get(p => p.Id == pageId && !p.Deleted);
        }

        public async Task<PageDto> GetViewById(int pageId)
        {
            var data = _pageDal.GetList(p => p.Id == pageId && !p.Deleted).Include(f => f.FeaturedImageFile);
            return await _mapper.ProjectTo<PageDto>(data).FirstOrDefaultAsync();
        }

        public async Task<PageDto> GetByUrl(string url)
        {
            var data = _pageDal.GetList(p => p.Url != null && p.Url.ToLower() == url.ToLower() && !p.Deleted).Include(f => f.FeaturedImageFile);
            return await _mapper.ProjectTo<PageDto>(data).FirstOrDefaultAsync();
        }

        public async Task Update(Page page)
        {
            await _pageDal.Update(page);
        }

        public async Task Delete(Page page)
        {
            await _pageDal.Delete(page);
        }

        public async Task Add(Page page)
        {
            await _pageDal.Add(page);
        }

        public async Task<List<PageDto>> GetList()
        {
            var list = _pageDal.GetList(p => !p.Deleted).Include(f => f.FeaturedImageFile);
            return await _mapper.ProjectTo<PageDto>(list).ToListAsync();
        }
    }
}
