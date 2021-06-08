using Entity.Dtos;
using Entity.Enums;
using Newtonsoft.Json;
using ServerService.Abstract;
using ServerService.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Concrete
{
    public class AaNewsManager : IAaNewsService
    {
        string token = "";
        public AaNewsManager()
        {
            token = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{AppSettingsHelper.AaUserName}:{AppSettingsHelper.AaPassword}"));
        }
        public void GetAaNews()
        {
            var list = new List<NewsAgencyAddDto>();
            var standardNews = GetAaStandardNews(0);
            if (standardNews != null && standardNews.Any())
            {
                list.AddRange(standardNews);
                AddArray(list);
            }
        }

        public List<NewsAgencyAddDto> GetAaStandardNews(int offset)
        {
            var list = new List<NewsAgencyAddDto>();
            var link = string.Format(AppSettingsHelper.AaStandardLink);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(link);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.Headers["Authorization"] = token;

            var bodyString = new StringBuilder();
            bodyString.Append(HttpUtility.UrlEncode("start_date") + "=" + HttpUtility.UrlEncode("") + "&");
            bodyString.Append(HttpUtility.UrlEncode("end_date") + "=" + HttpUtility.UrlEncode("NOW") + "&");
            bodyString.Append(HttpUtility.UrlEncode("search_string") + "=" + HttpUtility.UrlEncode("") + "&");
            bodyString.Append(HttpUtility.UrlEncode("filter_category") + "=" + HttpUtility.UrlEncode(AppSettingsHelper.AaFilterCategory) + "&");
            bodyString.Append(HttpUtility.UrlEncode("filter_priority") + "=" + HttpUtility.UrlEncode(AppSettingsHelper.AaFilterPriority) + "&");
            bodyString.Append(HttpUtility.UrlEncode("filter_package") + "=" + HttpUtility.UrlEncode(AppSettingsHelper.AaFilterPackage) + "&");
            bodyString.Append(HttpUtility.UrlEncode("filter_type") + "=" + HttpUtility.UrlEncode(AppSettingsHelper.AaFilterType) + "&");
            bodyString.Append(HttpUtility.UrlEncode("filter_language") + "=" + HttpUtility.UrlEncode(AppSettingsHelper.AaFilterLanguage) + "&");

            bodyString.Append(HttpUtility.UrlEncode("offset") + "=" + HttpUtility.UrlEncode($"{offset}") + "&");
            bodyString.Append(HttpUtility.UrlEncode("limit") + "=" + HttpUtility.UrlEncode(AppSettingsHelper.AaFilterLimit) + "&");
            string body = bodyString.ToString().Remove(bodyString.ToString().Length - 1, 1);

            if (!string.IsNullOrEmpty(body))
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(body);

                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            var response = (HttpWebResponse)httpWReq.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var result = JsonConvert.DeserializeObject<AaStandardNewsDto>(responseString);
            if (result != null && result.Response.Success)
            {
                foreach (var item in result.Data.Result)
                {
                    var splited = item.GroupId.StringIsNotNullOrEmpty() ? item.GroupId.Split(':') : item.Id.Split(':');
                    var id = splited[2] + ":" + splited[3];
                    if (!list.Any(f => f.Code == item.Id || f.Code == item.GroupId))
                    {
                        var details = GetAaNewsDetails(id);
                        if (details != null)
                        {
                            details.UpdateDate = item.Date.ToString().TurkishToUTCDate();
                            details.PublishDate = item.Date.ToString().TurkishToUTCDate();
                            details.Code = item.GroupId.StringIsNotNullOrEmpty() ? item.GroupId : item.Id;
                            details.NewsAgencyEntityId = NewsAgencyEntities.Aa;
                            details.Images = GetAaNewsPictures(id);
                            details.Videos = GetAaNewsVideos(id);
                            list.Add(details);
                        }
                    }
                }
            }
            return list;
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

        public NewsAgencyAddDto GetAaNewsDetails(string idInfo)
        {
            string uri = string.Format(AppSettingsHelper.AaTextLink, idInfo);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.Headers["Authorization"] = token;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return AaNewsHelper.GetNewsDetailsFromXml(responseString);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<AgencyNewsImageDto> GetAaNewsPictures(string idInfo)
        {
            string uri = string.Format(AppSettingsHelper.AaPictureLink, idInfo);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.Headers["Authorization"] = token;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var files = AaFileResponse.FromJson(responseString);
                    if (files.Response.Success)
                    {
                        return files.Data.Result.Select(f => new AgencyNewsImageDto
                        {
                            Url = f.Location.ToString()
                        }).ToList();
                    }
                }
            }
            catch (Exception ec)
            {
            }
            return null;
        }

        public List<AgencyNewsVideoDto> GetAaNewsVideos(string idInfo)
        {
            string uri = string.Format(AppSettingsHelper.AaVideoLink, idInfo);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.Headers["Authorization"] = token;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var files = AaFileResponse.FromJson(responseString);
                    if (files.Response.Success)
                    {
                        return files.Data.Result.Select(f => new AgencyNewsVideoDto
                        {
                            Url = f.Location.ToString()
                        }).ToList();
                    }
                }
            }
            catch (Exception ec)
            {
            }
            return null;
        }
    }
}