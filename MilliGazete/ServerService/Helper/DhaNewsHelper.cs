using Entity.Dtos;
using Entity.Enums;
using ServerService.Entity.Dto.Dha;
using System.Collections.Generic;
using System.Linq;

namespace ServerService.Helper
{
    public class DhaNewsHelper
    {
        public static List<NewsAgencyAddDto> ConvertXmlModelToDto(DhaStandardNewsDto.Rss result)
        {
            var list = new List<NewsAgencyAddDto>();
            foreach (var item in result.Channel.Item)
            {
                list.Add(new NewsAgencyAddDto()
                {
                    NewsAgencyEntityId = NewsAgencyEntities.Dha,
                    Category = item.Category.Replace("DHA-", "").Trim(),
                    City = item.Location,
                    Code = item.Guid,
                    Country = null,
                    Images = GetPhotos(item).Concat(GetPhotosHd(item)).ToList(),
                    ImageUpdateDate = item.PubDate.TurkishToUTCDate(),
                    LastMinute = "",//item.VideosExtensions.Text,
                    NewsId = null,
                    ParentCategory = "",
                    UpdateDate = item.PubDate.TurkishToUTCDate(),
                    Videos = GetVideos(item),
                    VideoUpdateDate = null,
                    Title = item.Title,
                    Description = item.Description,
                    PublishDate = item.PubDate.TurkishToUTCDate()
                });
            }
            return list;
        }

        public static List<AgencyNewsImageDto> GetAllPhotos(DhaStandardNewsDto.Item item)
        {
            var photosHd = GetPhotosHd(item);
            if (photosHd.Any()) return photosHd;
            else return GetPhotos(item);
        }

        public static List<AgencyNewsImageDto> GetPhotos(DhaStandardNewsDto.Item item)
        {
            var list = new List<AgencyNewsImageDto>();
            if (item.Photos != null && item.Photos.Any())
            {
                item.Photos.ForEach(file =>
                {
                    list.Add(new AgencyNewsImageDto
                    {
                        Url = file.Text,
                        Description = ""
                    });
                });
            }

            return list;
        }

        public static List<AgencyNewsImageDto> GetPhotosHd(DhaStandardNewsDto.Item item)
        {
            var list = new List<AgencyNewsImageDto>();
            if (item.PhotosHd != null && item.PhotosHd.Any())
            {
                item.PhotosHd.ForEach(file =>
                {
                    list.Add(new AgencyNewsImageDto
                    {
                        Url = file.Text,
                        Description = ""
                    });
                });
            }

            return list;
        }

        public static List<AgencyNewsVideoDto> GetVideos(DhaStandardNewsDto.Item item)
        {
            var list = new List<AgencyNewsVideoDto>();
            if (item.Video != null)
            {
                list.Add(new AgencyNewsVideoDto
                {
                    Url = item.Video.Text,
                    Description = item.VideosDescription.Text
                });
            }
            return list;
        }
    }
}
