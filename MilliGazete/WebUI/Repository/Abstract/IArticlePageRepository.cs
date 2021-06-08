using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface IArticlePageRepository : IUIBaseRepository
    {
        Task<IDataResult<ArticleDto>> GetArticleByUrl(string url);
        Task<IDataResult<List<ArticleDto>>> GetLastWeekMostViewedArticles(int count);
    }
}
