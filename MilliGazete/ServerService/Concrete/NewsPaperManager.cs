using Entity.Dtos;
using HtmlAgilityPack;
using ServerService.Abstract;
using ServerService.Helper;
using ServerService.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace ServerService.Concrete
{
    public class NewsPaperManager : INewsPaperService
    {
        public bool GetLastNewsPaperInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var listNewsPapers = new List<NewspaperAddDto>();

                    using (HttpResponseMessage response = client.GetAsync(AppSettingsHelper.NewsPaperLink).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = HttpUtility.HtmlDecode(content.ReadAsStringAsync().Result);
                            var resultat = new HtmlDocument();
                            resultat.LoadHtml(result);
                            var dateTag = resultat.DocumentNode.Descendants().Where(x => x.HasClass("gazeteler-tarih")).FirstOrDefault();
                            var newspaperDate = dateTag.InnerText.Split("-")[0];
                            var newsPaperDiv = resultat.DocumentNode.Descendants().Where(x => x.Name == "div" && x.HasClass("newspaper-list")).FirstOrDefault();
                            var figures = newsPaperDiv.Descendants().Where(x => x.Name == "figure").ToList();
                            foreach (var row in figures)
                            {
                                var aTag = row.Descendants().Where(f => f.Name == "a" && f.HasClass("newspaper-list-item")).FirstOrDefault();
                                var figcaption = row.Descendants().Where(f => f.Name == "div" && f.HasClass("figcaption")).FirstOrDefault();
                                var title = figcaption.Descendants().Where(f => f.Name == "span").FirstOrDefault();
                                if (aTag != null)
                                {
                                    var img = row.Descendants().Where(f => f.Name == "img").FirstOrDefault();

                                    listNewsPapers.Add(new NewspaperAddDto
                                    {
                                        MainImageUrl = aTag.Attributes["href"].Value,
                                        ThumbnailUrl = img == null ? "" : img.Attributes["src"].Value,
                                        Name = title?.InnerHtml,
                                        NewspaperDate = newspaperDate
                                    });
                                }
                            }
                        }
                    }
                    return NewsPaperHelper.AddArrayNewsPapers(listNewsPapers);
                }
                catch (Exception ec)
                {
                    throw;
                }
            }
        }
    }
}
