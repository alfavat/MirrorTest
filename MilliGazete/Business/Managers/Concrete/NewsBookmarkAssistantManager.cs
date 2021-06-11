using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsBookmarkAssistantManager : INewsBookmarkAssistantService
    {
        private readonly INewsBookmarkDal _newsBookmarkDal;
        private readonly IMapper _mapper;
        public NewsBookmarkAssistantManager(INewsBookmarkDal NewsBookmarkDal, IMapper mapper)
        {
            _newsBookmarkDal = NewsBookmarkDal;
            _mapper = mapper;
        }

        public async Task<NewsBookmark> GetById(int newsId, int requestUserId)
        {
            return await _newsBookmarkDal.Get(p => p.NewsId == newsId && p.UserId == requestUserId);
        }


        public async Task<NewsBookmarkDto> GetByNewsUrl(string url, int requestUserId)
        {
            var data = await _newsBookmarkDal.GetList(f => f.News.Url.ToLower() == url.ToLower() && f.UserId == requestUserId && !f.News.Deleted)
                .Include(f => f.News).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.News).ThenInclude(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.User).FirstOrDefaultAsync();

            return _mapper.Map<NewsBookmarkDto>(data);
        }

        public async Task<NewsBookmarkDto> GetByNewsId(int newsId, int requestUserId)
        {
            var data = await _newsBookmarkDal.GetList(f => f.NewsId == newsId && !f.News.Deleted && f.UserId == requestUserId)
                .Include(f => f.News).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.News).ThenInclude(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.User).FirstOrDefaultAsync();

            return _mapper.Map<NewsBookmarkDto>(data);
        }

        public async Task<bool> HasUserBookmarkedNews(int newsId, int requestUserId)
        {
            return await _newsBookmarkDal.GetList(f => f.NewsId == newsId && !f.News.Deleted && f.UserId == requestUserId).AnyAsync();
        }

        public async Task DeleteByNewsId(int newsId, int requestUserId)
        {
            var data = await _newsBookmarkDal.GetNewsBookmarks(f => f.NewsId == newsId && f.UserId == requestUserId).ToListAsync();
            await _newsBookmarkDal.RemoveRange(data);
        }

        public async Task Add(NewsBookmark newsBookmark)
        {
            await _newsBookmarkDal.Add(newsBookmark);
        }

        public async Task<List<NewsBookmarkDto>> GetList(int requestUserId)
        {
            var list = _newsBookmarkDal.GetList(f => !f.News.Deleted && f.UserId == requestUserId)
                .Include(f => f.News).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.News).ThenInclude(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.User);

            return await _mapper.ProjectTo<NewsBookmarkDto>(list).ToListAsync();
        }
    }
}
