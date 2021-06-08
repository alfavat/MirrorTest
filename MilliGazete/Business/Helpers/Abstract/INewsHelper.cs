using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface INewsHelper
    {
        List<NewsViewDto> ShortenDescription(List<NewsViewDto> list);
        List<NewsFileDto> OrderEntities(List<NewsFileDto> data);
        List<NewsViewDto> FixUrls(List<NewsViewDto> data);
        NewsViewDto FixUrl(NewsViewDto dto);
    }
}
