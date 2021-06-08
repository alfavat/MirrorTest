using Entity.Dtos;
using ServerService.Abstract;
using ServerService.Entity.Dto.Dha;
using ServerService.Helper;
using System;
using System.Collections.Generic;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Concrete
{
    public class DhaNewsManager : IDhaNewsService
    {
        public void GetDhaNews()
        {
            var list = new List<NewsAgencyAddDto>();
            var standardNews = GetDhaStandardNews();
            list.AddRange(standardNews);
            AddArray(list);
        }

        public List<NewsAgencyAddDto> GetDhaStandardNews()
        {
            var link = string.Format(AppSettingsHelper.DhaStandardLink);
            var result = XmlHelper.GetModelFromXml<DhaStandardNewsDto.Rss>(link);
            return DhaNewsHelper.ConvertXmlModelToDto(result);
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
