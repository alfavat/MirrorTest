using Business.Constants;
using Business.Helpers.Abstract;
using Business.Managers.Abstract;
using Entity.Dtos;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Helpers.Concrete
{
    public class NewsHelper : INewsHelper
    {
        private readonly IBaseService _baseService;

        public NewsHelper(IBaseService baseService)
        {
            _baseService = baseService;
        }
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

        public bool CheckNewsPushNotification(NewsAddDto dto, out string msg)
        {
            if (!dto.Active || dto.IsDraft)
            {
                msg = Messages.PushNotificationActiveDraftError;
                return false;
            }
            if (dto.PublishDate.StringIsNullOrEmpty() && dto.PublishTime.StringIsNullOrEmpty())
            {
                msg = Messages.PushNotificationPublishDateError;
                return false;
            }
            var publishDateTime = DateTime.Parse(dto.PublishDate).Date.Add(TimeSpan.Parse(dto.PublishTime));
            if (publishDateTime > DateTime.Now.AddMinutes(5))
            {
                msg = Messages.PushNotificationPublishDateError;
                return false;
            }
            msg = "";
            return true;
        }

        public bool CheckFlashNews(NewsAddDto dto, out string msg)
        {
            if (!dto.Active || dto.IsDraft)
            {
                msg = Messages.FlashNewsActiveDraftError;
                return false;
            }
            if (dto.PublishDate.StringIsNullOrEmpty() && dto.PublishTime.StringIsNullOrEmpty())
            {
                msg = Messages.FlashNewsPublishDateError;
                return false;
            }
            var publishDateTime = DateTime.Parse(dto.PublishDate).Date.Add(TimeSpan.Parse(dto.PublishTime));
            if (publishDateTime > DateTime.Now.AddMinutes(5)) // publish date must be before current time  
            {
                msg = Messages.FlashNewsPublishDateError;
                return false;
            }
            var flashNewsMinutes =AppSettingsExtension.GetValue<int>("FlashNewsMinutes");
            if (publishDateTime < DateTime.Now.AddMinutes(-1 * flashNewsMinutes)) // publish date must be bigger than 15(FlashNewsMinutes) minutes ago
            {
                msg = Messages.FlashNewsPublishDateError;
                return false;
            }
            msg = "";
            return true;
        }
    }
}
