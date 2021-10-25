using Entity.Dtos;
using ServerService.Abstract;
using ServerService.Helper;
using ServerService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ServerService.Concrete
{
    public class PrayerTimeManager : IPrayerTimeService
    {
        public bool GetLastPrayerTimeInfo()
        {
            try
            {
                var cityList = CityHelper.GetCityList();
                var listPrayerTimes = new List<PrayerTimeAddDto>();
                foreach (var item in cityList)
                {
                    try
                    {
                        var cityId = item.Id;
                        string cityName = item.Name.ToLower().ToEnglishCharacter().ToLower();
                        string urlAddress = AppSettingsHelper.PrayerTimeLink;

                        urlAddress += $"{cityName}-namaz-vakitleri";

                        using (HttpClient client = new HttpClient())
                        {
                            using (HttpResponseMessage response = client.GetAsync(urlAddress).Result)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = content.ReadAsStringAsync().Result;
                                    var resultat = new HtmlAgilityPack.HtmlDocument();
                                    resultat.LoadHtml(result);
                                    var currentMonth = "month" + DateTime.Now.Month.ToString();
                                    var currentMonthTable = resultat.DocumentNode.Descendants().Where(x => x.Name == "table"
                                    && x.Attributes["id"] != null && x.Attributes["id"].Value == currentMonth).FirstOrDefault();
                                    var tbody = currentMonthTable.Descendants().Where(x => x.Name == "tbody").FirstOrDefault();
                                    var trs = tbody.Descendants().Where(x => x.Name == "tr").ToList();
                                    foreach (var row in trs)
                                    {
                                        var columns = row.Descendants().Where(f => f.Name == "td").ToList();
                                        var prayerDate = columns[0].InnerText;
                                        string strDawnTime = columns[1].InnerText;
                                        string strSunTime = columns[2].InnerText;
                                        string strNoonPrayer = columns[3].InnerText;
                                        string strAfternoonPrayer = columns[4].InnerText;
                                        string strEveningPrayer = columns[5].InnerText;
                                        string strNightPrayer = columns[6].InnerText;

                                        listPrayerTimes.Add(new PrayerTimeAddDto
                                        {
                                            CityId = cityId,
                                            PrayerDate = prayerDate,
                                            DawnTime = strDawnTime,
                                            SunTime = strSunTime,
                                            NoonPrayer = strNoonPrayer,
                                            AfternoonPrayer = strAfternoonPrayer,
                                            EveningPrayer = strEveningPrayer,
                                            NightPrayer = strNightPrayer
                                        });

                                    }
                                }
                            }

                        }
                    }
                    catch { }
                }
                return PrayerTimeHelper.AddArrayPrayerTimes(listPrayerTimes);
            }
            catch
            {
                throw;
            }
        }
    }
}
