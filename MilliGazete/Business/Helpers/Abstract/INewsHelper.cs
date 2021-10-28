using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface INewsHelper
    {
        List<NewsPagingViewDto> ShortenDescription(List<NewsPagingViewDto> list);
        List<NewsFileDto> OrderEntities(List<NewsFileDto> data);
        List<NewsViewDto> FixUrls(List<NewsViewDto> data);
        NewsViewDto FixUrl(NewsViewDto dto);
        bool CheckNewsPushNotification(NewsAddDto dto, out string msg);
        bool CheckFlashNews(NewsAddDto dto, out string msg);
    }
}
