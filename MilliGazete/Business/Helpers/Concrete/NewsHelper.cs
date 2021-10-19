using Business.Helpers.Abstract;
using Entity.Dtos;
using Entity.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Business.Helpers.Concrete
{
    public class NewsHelper : INewsHelper
    {
        public List<NewsViewDto> FixUrls(List<NewsViewDto> list)
        {
            if (!list.HasValue())
            {
                return new List<NewsViewDto>();
            }
            list.ForEach(f =>
            {
                if (f.Url.StringNotNullOrEmpty())
                {
                    f.Url = f.Url.Replace(@"//", "");
                }
                if (f.NewsTypeEntityId == (int)NewsTypeEntities.Article)
                {
                    f.Url = "/makale/" + f.Url;
                }
                else f.Url = "/" + f.NewsCategoryList.FirstOrDefault()?.Category?.Url + "/" + f.Url + "-" + f.HistoryNo.ToString();
            });
            return list;
        }

        public NewsViewDto FixUrl(NewsViewDto dto)
        {
            if (dto == null)
            {
                return new NewsViewDto();
            }

            var splited = dto.Url.Split('/').ToList();
            dto.Url = splited.HasValue() ? splited.Last() : dto.Url;
            return dto;
        }

        public List<NewsFileDto> OrderEntities(List<NewsFileDto> data)
        {
            if (!data.HasValue())
            {
                return new List<NewsFileDto>();
            }

            return data.OrderBy(f => f.Order).ToList();
        }     

        public List<NewsPagingViewDto> ShortenDescription(List<NewsPagingViewDto> list)
        {
            if (!list.HasValue())
            {
                return list;
            }
            list.ForEach(f =>
            {
                f.ShortDescription = f.ShortDescription.ShortenText(150);
            });
            return list;
        }
    }
}
