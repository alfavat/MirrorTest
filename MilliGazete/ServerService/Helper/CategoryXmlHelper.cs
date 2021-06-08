using Entity.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Helper
{
    public static class CategoryXmlHelper
    {
        public static List<NewsSiteMapDto> GetCategoryList()
        {
            try
            {
                using (HttpHelper http = new HttpHelper())
                {
                    var saveRequest = http.Request<dynamic>(new RequestObject()
                    {
                        Url = AppSettingsHelper.ApiLink + "news/getlistforsitemap",
                        AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                        Method = "GET"
                    });
                    var data = JsonConvert.DeserializeObject<List<NewsSiteMapDto>>(saveRequest.data.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateCategoriesXml()
        {
            var list = GetCategoryList();
            CreateXml(AppSettingsHelper.WebUIMapBasePath, AppSettingsHelper.MainUrl, list);
            CreateXml(AppSettingsHelper.MobileUIBasePath, AppSettingsHelper.MobileMainUrl, list);
        }

        private static void CreateXml(string basePath, string mainUrl, List<NewsSiteMapDto> list)
        {
            string xNamespace = AppSettingsHelper.XmlNamespace,
                xsi = AppSettingsHelper.Xsi,
             schemeLocation = AppSettingsHelper.SchemeLocation;

            using (XmlWriter writer = XmlWriter.Create(basePath + "categories.xml", new XmlWriterSettings
            {
                Encoding = System.Text.Encoding.UTF8
            }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("sitemapindex", xNamespace);
                writer.WriteAttributeString("xmlns", xNamespace);
                foreach (var item in list)
                {
                    if (!Directory.Exists(basePath + item.CategoryName))
                    {
                        Directory.CreateDirectory(basePath + item.CategoryName);
                    }
                    writer.WriteStartElement("sitemap");
                    writer.WriteElementString("loc", mainUrl + "/" + item.CategoryName);
                    writer.WriteElementString("changefreq", "hourly");
                    writer.WriteEndElement();

                    foreach (var child in item.Items)
                    {
                        writer.WriteStartElement("sitemap");
                        writer.WriteElementString("loc", mainUrl + "/" + item.CategoryName + "/" + child.YearAndMonth + ".xml");
                        writer.WriteElementString("changefreq", "hourly");
                        writer.WriteEndElement();
                        using (XmlWriter childWriter = XmlWriter.Create(basePath + item.CategoryName + "/" + child.YearAndMonth + ".xml", new XmlWriterSettings
                        {
                            Encoding = System.Text.Encoding.UTF8
                        }))
                        {

                            childWriter.WriteStartDocument();
                            childWriter.WriteStartElement("urlset", xNamespace);
                            childWriter.WriteAttributeString("xmlns", xNamespace);
                            childWriter.WriteAttributeString("xmlns", "xsi", null, xsi);
                            childWriter.WriteAttributeString("xsi", "schemaLocation", null, schemeLocation);
                            if (child.NewsList != null)
                            {
                                foreach (var news in child.NewsList)
                                {
                                    childWriter.WriteStartElement("url");

                                    childWriter.WriteElementString("loc", mainUrl + news.Url);
                                    childWriter.WriteElementString("lastmod", news.LastMod.ToString("yyyy-MM-ddTHH:mm:ss"));

                                    childWriter.WriteStartElement("news");
                                    childWriter.WriteStartElement("publication");
                                    childWriter.WriteElementString("name", news.AgencyName);
                                    childWriter.WriteElementString("language", "tr");
                                    childWriter.WriteEndElement();
                                    childWriter.WriteElementString("publication_date", news.PulishDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                                    childWriter.WriteElementString("title", news.Title);
                                    childWriter.WriteElementString("keywords", news.Keywords);
                                    childWriter.WriteEndElement();

                                    childWriter.WriteStartElement("image");
                                    childWriter.WriteElementString("loc", news.ImageUrl);
                                    childWriter.WriteEndElement();

                                    childWriter.WriteEndElement();
                                }
                            }
                            childWriter.WriteEndElement();
                            childWriter.WriteEndDocument();

                        }
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
