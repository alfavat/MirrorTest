using Entity.Dtos;
using ServerService.Abstract;
using ServerService.Entity.Dto.Iha;
using ServerService.Helper;
using System;
using System.Collections.Generic;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Concrete
{
    public class IhaNewsManager : IIhaNewsService
    {
        public void GetIhaNews()
        {
            var list = new List<NewsAgencyAddDto>();

            var standardNews = GetIhaStandardNews();
            var internetNews = GetIhaInternetNews();

            list.AddRange(standardNews);
            list.AddRange(internetNews);

            AddArray(list);
        }

        public List<NewsAgencyAddDto> GetIhaStandardNews()
        {
            var link = string.Format(AppSettingsHelper.IhaStandardLink,
                AppSettingsHelper.IhaUserCode,
                AppSettingsHelper.IhaUserName,
                AppSettingsHelper.IhaPassword, 1, 0, 0, 0, 0, 0);
            var result = XmlHelper.GetModelFromXml<IhaStandardNewsDto.Rss>(link);
            return IhaNewsHelper.ConvertXmlModelToDto(result);
        }

        public List<NewsAgencyAddDto> GetIhaInternetNews()
        {
            var link = string.Format(AppSettingsHelper.IhaInternetLink,
                AppSettingsHelper.IhaUserCode,
                AppSettingsHelper.IhaUserName,
                AppSettingsHelper.IhaPassword, 1, 0, 0, 0, 0, 2, 2);
            var result = XmlHelper.GetModelFromXml<IhaInternetNewsDto.Rss>(link);
            return IhaNewsHelper.ConvertXmlModelToDto(result);
        }

        private void AddArray(List<NewsAgencyAddDto> list)
        {
            try
            {
                using (HttpHelper http = new HttpHelper())
                {
                    var saveRequest = http.Request<dynamic>(new RequestObject()
                    {
                        Url = AppSettingsHelper.ApiLink + "agencynews/addarray",
                        AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                        Method = "POST",
                        Body = list
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
