using Core.Utilities.Results;
using Entity.Dtos;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class NewsHitRepository : INewsHitRepository
    {
        public IResult Add(int newsId, int newsHitComeFromEntityId)
        {
            NewsHitAddDto newsHitAdd = new NewsHitAddDto();
            newsHitAdd.NewsId = newsId;
            newsHitAdd.NewsHitComeFromEntityId = newsHitComeFromEntityId;
            return ApiHelper.PostNoDataApi("NewsHits/add", newsHitAdd);
        }
    }
}
