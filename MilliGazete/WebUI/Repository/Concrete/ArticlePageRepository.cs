using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class ArticlePageRepository : IArticlePageRepository
    {
        public async Task<IDataResult<ArticleDto>> GetArticleByUrl(string url)
        {
            return await ApiHelper.GetApiAsync<ArticleDto>("articles/getbyurl?url=" + url);
        }

        public async Task<IDataResult<List<ArticleDto>>> GetLastWeekMostViewedArticles(int count)
        {
            return await ApiHelper.GetApiAsync<List<ArticleDto>>("articles/getlastweekmostviewed?count=" + count);
        }
    }
}
