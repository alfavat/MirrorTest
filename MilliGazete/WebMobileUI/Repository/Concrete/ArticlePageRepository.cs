using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class ArticlePageRepository : IArticlePageRepository
    {
        public IDataResult<ArticleDto> GetArticleByUrl(string url)
        {
            return ApiHelper.GetApi<ArticleDto>("articles/getbyurl?url=" + url);
        }

        public IDataResult<List<ArticleDto>> GetLastWeekMostViewedArticles(int count)
        {
            return ApiHelper.GetApi<List<ArticleDto>>("articles/getlastweekmostviewed?count=" + count);
        }
    }
}
