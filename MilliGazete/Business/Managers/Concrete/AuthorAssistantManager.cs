using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class AuthorAssistantManager : IAuthorAssistantService
    {
        private readonly IAuthorDal _authorDal;
        private readonly INewsDal _newsDal;
        private readonly IMapper _mapper;
        public AuthorAssistantManager(IAuthorDal AuthorDal, INewsDal newsDal, IMapper mapper)
        {
            _authorDal = AuthorDal;
            _newsDal = newsDal;
            _mapper = mapper;
        }

        public async Task<Author> GetById(int id)
        {
            return await _authorDal.Get(p => p.Id == id && !p.Deleted);
        }

        public async Task Update(Author author)
        {
            await _authorDal.Update(author);
        }

        public async Task Delete(Author author)
        {
            await _authorDal.Delete(author);
        }

        public async Task Add(Author author)
        {
            await _authorDal.Add(author);
        }

        public async Task<List<AuthorDto>> GetList()
        {
            var list = _authorDal.GetList(p => !p.Deleted)
                .Include(f => f.FeaturedImageFile)
                .Include(f => f.PhotoFile);
            var authors = await _mapper.ProjectTo<AuthorDto>(list).ToListAsync();
            if (authors.HasValue())
            {
                foreach (var item in authors)
                {
                    var article = await _newsDal.GetActiveList().Where(f => f.AuthorId == item.Id)
                        .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                        .Select(f => new { f.AuthorId, f.Title, Url = f.GetUrl(), f.PublishDate })
                        .OrderByDescending(f => f.PublishDate).FirstOrDefaultAsync();
                    if (article != null)
                    {
                        item.LastArticleDate = article.PublishDate;
                        item.LastArticleTitle = article.Title;
                        item.LastArticleUrl = article.Url;
                    }
                }
            }
            return authors;
        }

        public async Task<AuthorDto> GetViewById(int id)
        {
            var data = await _authorDal.GetList(p => !p.Deleted && p.Id == id)
                .Include(f => f.FeaturedImageFile)
                .Include(f => f.PhotoFile).FirstOrDefaultAsync();
            var item = _mapper.Map<AuthorDto>(data);
            var article = await _newsDal.GetActiveList().Where(f => f.AuthorId == item.Id)
                        .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                        .Select(f => new { f.AuthorId, f.Title, Url = f.GetUrl(), f.PublishDate })
                        .OrderByDescending(f => f.PublishDate).FirstOrDefaultAsync();
            if (article != null)
            {
                item.LastArticleDate = article.PublishDate;
                item.LastArticleTitle = article.Title;
                item.LastArticleUrl = article.Url;
            }
            return item;
        }

        public async Task<AuthorWithDetailsDto> GetViewByName(string nameSurename)
        {
            var data = await _authorDal.GetList(p => !p.Deleted && p.NameSurename.ToLower().Trim() == nameSurename.ToLower().Trim())
                .Include(f => f.FeaturedImageFile)
                .Include(f => f.PhotoFile)
                .FirstOrDefaultAsync();
            var item = _mapper.Map<AuthorWithDetailsDto>(data);
            var articles = _newsDal.GetActiveList().Where(f => f.AuthorId == item.Id)
                        .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                        .OrderByDescending(f => f.PublishDate);
            if (articles != null)
            {
                item.ArticleList = await _mapper.ProjectTo<ArticleDto>(articles).ToListAsync();
            }
            return item;
        }

        public async Task<Author> CheckAuthor(AuthorAddDto dto)
        {
            return await _authorDal.Get(f => !f.Deleted && f.NameSurename.ToLower() == dto.NameSurename.ToLower() ||
            f.Email.ToLower() == dto.Email.ToLower() ||
            f.Url.ToLower() == dto.Url.ToLower());
        }
    }
}
