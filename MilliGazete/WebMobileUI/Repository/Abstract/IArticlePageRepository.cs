using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;

namespace WebMobileUI.Repository.Abstract
{
    public interface IArticlePageRepository : IUIBaseRepository
    {
        IDataResult<ArticleDto> GetArticleByUrl(string url);
        IDataResult<List<ArticleDto>> GetLastWeekMostViewedArticles(int count);
    }
}
