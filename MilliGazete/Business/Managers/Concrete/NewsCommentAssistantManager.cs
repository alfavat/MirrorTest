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
    public class NewsCommentAssistantManager : INewsCommentAssistantService
    {
        private readonly INewsCommentDal _newsCommentDal;
        private readonly IMapper _mapper;
        public NewsCommentAssistantManager(INewsCommentDal NewsCommentDal, IMapper mapper)
        {
            _newsCommentDal = NewsCommentDal;
            _mapper = mapper;
        }
        public List<NewsCommentDto> GetListByPaging(NewsCommentPagingDto pagingDto, out int total)
        {
            var query = _newsCommentDal.GetList(f => !f.Deleted)
                .Include(f => f.News).ThenInclude(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.User).AsQueryable();

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.Content.Contains(pagingDto.Query) || f.Title.Contains(pagingDto.Query));

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            if (pagingDto.Approved.HasValue)
                query = query.Where(f => f.Approved == pagingDto.Approved);


            if (pagingDto.NewsId.HasValue)
                query = query.Where(f => f.NewsId == pagingDto.NewsId);

            if (pagingDto.UserId.HasValue)
                query = query.Where(f => f.UserId == pagingDto.UserId);


            total = query.Count();
            var list = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return _mapper.ProjectTo<NewsCommentDto>(list).ToList();
        }

        public async Task<NewsComment> GetById(int newsCommentId)
        {
            return await _newsCommentDal.Get(p => p.Id == newsCommentId && !p.Deleted);
        }

        public async Task Update(NewsComment newsComment)
        {
            await _newsCommentDal.Update(newsComment);
        }

        public async Task Delete(NewsComment newsComment)
        {
            await _newsCommentDal.Delete(newsComment);
        }

        public async Task Add(NewsComment newsComment)
        {
            await _newsCommentDal.Add(newsComment); }

        public async Task<List<NewsCommentDto>> GetList()
        {
            var list = _newsCommentDal.GetList(p => !p.Deleted).Include(f => f.User);
            return await _mapper.ProjectTo<NewsCommentDto>(list).ToListAsync();
        }

        public List<UserNewsCommentDto> GetByNewsId(int newsId, int limit, int page, out int total)
        {
            var query = _newsCommentDal.GetList(p => !p.Deleted && p.NewsId == newsId && p.Approved)
                .Include(f => f.NewsCommentLikes)
                .Include(f => f.User);

            total = query.Count();
            var list = query.OrderBy(f => f.CreatedAt).Skip((page - 1) * limit.CheckLimit()).Take(limit.CheckLimit());
            return _mapper.ProjectTo<UserNewsCommentDto>(list).ToList();
        }
    }
}
