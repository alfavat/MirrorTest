using Entity.Dtos;
using Entity.Enums;
using ServerService.Entity.Dto.Iha;
using System.Collections.Generic;
using System.Linq;

namespace ServerService.Helper
{
    public class IhaNewsHelper
    {
        public static List<NewsAgencyAddDto> ConvertXmlModelToDto(IhaStandardNewsDto.Rss result)
        {
            var list = new List<NewsAgencyAddDto>();
            foreach (var item in result.Channel.Item)
            {
                list.Add(new NewsAgencyAddDto()
                {
                    NewsAgencyEntityId = NewsAgencyEntities.Iha,
                    Category = item.Kategori,
                    City = item.Sehir,
                    Code = item.HaberKodu,
                    Country = null,
                    Images = GetImages(item),
                    ImageUpdateDate = item.SonFotografEklenmeTarihi.TurkishToUTCDate(),
                    LastMinute = item.SonDakika,
                    NewsId = null,
                    ParentCategory = item.UstKategori,
                    UpdateDate = item.SonHaberGuncellenmeTarihi.TurkishToUTCDate(),
                    Videos = GetVideos(item),
                    VideoUpdateDate = null,
                    Title = item.Title,
                    Description = item.Description,
                    PublishDate = item.PubDate.TurkishToUTCDate()
                });
            }
            return list;
        }

        public static List<NewsAgencyAddDto> ConvertXmlModelToDto(IhaInternetNewsDto.Rss result)
        {
            var list = new List<NewsAgencyAddDto>();
            foreach (var item in result.Channel.Item)
            {
                list.Add(new NewsAgencyAddDto()
                {
                    NewsAgencyEntityId = NewsAgencyEntities.Iha,
                    Category = item.Kategori,
                    City = item.Sehir,
                    Code = item.HaberID.Text,
                    Country = item.Ulke,
                    Images = IhaNewsHelper.GetImages(item),
                    ImageUpdateDate = item.SonFotografEklenmeTarihi.TurkishToUTCDate(),
                    LastMinute = null,
                    NewsId = item.HaberID.Text.ToInt32(),
                    ParentCategory = null,
                    UpdateDate = item.SonHaberGuncellenmeTarihi.TurkishToUTCDate(),
                    Videos = IhaNewsHelper.GetVideos(item),
                    VideoUpdateDate = item.SonVideoEklenmeTarihi.TurkishToUTCDate(),
                    Title = item.Title,
                    Description = item.Description,
                    PublishDate = item.PubDate.TurkishToUTCDate()
                });
            }
            return list;
        }

        public static List<AgencyNewsImageDto> GetImages(IhaStandardNewsDto.Item item)
        {
            var list = new List<AgencyNewsImageDto>();
            if (item.Images != null && item.Images.Image != null)
            {
                item.Images.Image.ForEach(file =>
                {
                    string desc = null;
                    if (item.Aciklamalar != null && item.Aciklamalar.Aciklama != null)
                    {
                        var d = item.Aciklamalar.Aciklama.FirstOrDefault(f => f.ResimKodu == file.ResimKodu && f.HaberKodu == file.HaberKodu);
                        if (d != null)
                        {
                            desc = d.Text;
                        }
                    }
                    list.Add(new AgencyNewsImageDto
                    {
                        Url = file.Text,
                        Description = desc
                    });
                });
            }

            return list;
        }

        public static List<AgencyNewsVideoDto> GetVideos(IhaStandardNewsDto.Item item)
        {
            var list = new List<AgencyNewsVideoDto>();
            if (item.Videos != null && item.Videos.Video != null)
            {
                list.Add(new AgencyNewsVideoDto
                {
                    Url = item.Videos.Video.Path_video.Text,
                    Description = item.Videos.Video.Aciklama.Text
                });
            }
            return list;
        }

        public static List<AgencyNewsImageDto> GetImages(IhaInternetNewsDto.Item item)
        {
            var list = new List<AgencyNewsImageDto>();
            if (item.Images != null && item.Images.Image != null)
            {
                item.Images.Image.ForEach(file =>
                {
                    list.Add(new AgencyNewsImageDto
                    {
                        Url = file.Path.Text
                    });
                });
            }

            return list;
        }

        public static List<AgencyNewsVideoDto> GetVideos(IhaInternetNewsDto.Item item)
        {
            var list = new List<AgencyNewsVideoDto>();
            if (item.Videos != null && item.Videos.Video != null)
            {
                list.Add(new AgencyNewsVideoDto
                {
                    Url = item.Videos.Video.Path.Text
                });
            }
            return list;
        }
    }
}
