using Core.Utilities.Results;
using Entity.Dtos;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class NewsHitRepository : INewsHitRepository
    {
        public async Task<IResult> Add(int newsId, int newsHitComeFromEntityId)
        {
            NewsHitAddDto newsHitAdd = new NewsHitAddDto();
            newsHitAdd.NewsId = newsId;
            newsHitAdd.NewsHitComeFromEntityId = newsHitComeFromEntityId;
            return await ApiHelper.PostNoDataApiAsync("NewsHits/add", newsHitAdd);
        }
    }
}
