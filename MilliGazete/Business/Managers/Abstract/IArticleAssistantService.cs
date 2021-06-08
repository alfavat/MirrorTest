using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IArticleAssistantService
    {
        Task<Article> GetById(int articleId);
        Task<ArticleDto> GetByUrl(string url);
        Task Update(Article article);
        Task Delete(Article article);
        Task<List<ArticleDto>> GetList();
        Task Add(Article article);
        List<ArticleDto> GetListByPaging(ArticlePagingDto pagingDto, out int total);
        Task<List<ArticleDto>> GetListByAuthorId(int authorId);
        Task<List<ArticleDto>> GetLastWeekMostViewedArticles(int count);
    }
}
