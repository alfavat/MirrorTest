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
    public class ArticleAssistantManager : IArticleAssistantService
    {
        private readonly IArticleDal _articleDal;
        private readonly IMapper _mapper;
        public ArticleAssistantManager(IArticleDal articleDal, IMapper mapper)
        {
            _articleDal = articleDal;
            _mapper = mapper;
        }
        public List<ArticleDto> GetListByPaging(ArticlePagingDto pagingDto, out int total)
        {
            var list = _articleDal.GetList(f => !f.Deleted).Include(f => f.Author).ThenInclude(f => f.PhotoFile);
            var query = _mapper.ProjectTo<ArticleDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.SeoTitle.Contains(pagingDto.Query) || f.Url.Contains(pagingDto.Query) ||
                f.SeoDescription.Contains(pagingDto.Query) || f.SeoKeywords.Contains(pagingDto.Query));

            if (pagingDto.Approved.HasValue)
                query = query.Where(f => f.Approved == pagingDto.Approved);


            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<Article> GetById(int articleId)
        {
            return await _articleDal.Get(p => p.Id == articleId && !p.Deleted);
        }

        public async Task<ArticleDto> GetByUrl(string url)
        {
            var data = await _articleDal.GetList(p => p.Url != null && p.Url.ToLower() == url.ToLower() && !p.Deleted && !p.Author.Deleted)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .FirstOrDefaultAsync();
            return _mapper.Map<ArticleDto>(data);
        }

        public async Task Update(Article article)
        {
            await _articleDal.Update(article);
        }

        public async Task Delete(Article article)
        {
            await _articleDal.Delete(article);
        }

        public async Task Add(Article article)
        {
            await _articleDal.Add(article);
        }

        public async Task<List<ArticleDto>> GetList()
        {
            var list = _articleDal.GetList(p => !p.Deleted && !p.Author.Deleted).Include(f => f.Author).ThenInclude(f => f.PhotoFile);
            return await _mapper.ProjectTo<ArticleDto>(list).ToListAsync();
        }

        public async Task<List<ArticleDto>> GetListByAuthorId(int authorId)
        {
            var list = _articleDal.GetList(p => !p.Deleted && p.AuthorId == authorId && !p.Author.Deleted).Include(f => f.Author).ThenInclude(f => f.PhotoFile);
            return await _mapper.ProjectTo<ArticleDto>(list).ToListAsync();
        }

        public async Task<List<ArticleDto>> GetLastWeekMostViewedArticles(int count)
        {
            var list = _articleDal.GetList(p => !p.Deleted && !p.Author.Deleted)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .OrderByDescending(f => f.ReadCount).Take(count.CheckLimit());
            return await _mapper.ProjectTo<ArticleDto>(list).ToListAsync();
        }
    }
}
